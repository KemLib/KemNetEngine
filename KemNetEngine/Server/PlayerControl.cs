using KemLibCore.Concurrent.Inter;
using KemLibCore.Concurrent.Locker;
using KemLibCore.Concurrent.Queue;
using KemLibCore.DebugLog;
using KemNetEngine.Network;
using System.Net;
using System.Net.Sockets;

namespace KemNetEngine.Server
{
    public class PlayerControl
    {
        #region Properties
        private const string LOG_CALL_ON_START_FAIL = "Call method OnStart fail",
            LOG_CALL_ON_UPDATE_FAIL = "Call method OnUpdate fail",
            LOG_CALL_ON_STOP_FAIL = "Call method OnStop fail",
            LOG_CALL_ON_DISCONNECT_FAIL = "Call method OnDisconnect fail",
            LOG_CALL_ON_RECEIVE_FAIL = "Call method OnReceive fail",
            LOG_CALL_ON_SEND_FAIL = "Call method OnSend fail",
            LOG_CALL_ON_SEND_T_FAIL = "Call method OnSend<{0}> fail",
            LOG_CALL_ON_SEND_T1_T2_FAIL = "Call method OnSend<{0}, {1}> fail",
            LOG_CALL_ON_SEND_T1_T2_T3_FAIL = "Call method OnSend<{0}, {1}, {2}> fail",
            LOG_CALL_ON_SEND_T1_T2_T3_T4_FAIL = "Call method OnSend<{0}, {1}, {2}, {3}> fail";
        private const string LOG_CRATE_PLAYER_HANDER_FAIL = "Create player hander fail, player will be closed",
            LOG_SOCKET_CLOSE_FAIL = "Socket close fail";
        private const string ERROR_CALL_ON_SEND_FAIL = "({0}) {1}",
            ERROR_SEND_NOT_CONNET_CLIENT_FAIL = "Player not connet client";
        private const string NAME_DEFAULT = "Player",
            NAME_FORMAT = "Player({0})";

        public readonly PlayerManager PlayerManager;
        private readonly Socket socketClient;
        private readonly IQueueIn<IEvent> queueReceive,
            queueSend;
        private readonly ILogHander logHander;
        public readonly string Name;
        private readonly CancellationTokenSource cancellationTokenSource;
        private InterBoolStruct isConnect;
        private IPlayerHander playerHander;
        private readonly int requestMessagePerFrame;
        private InterIntStruct requestMessage;
        private InterObjectStruct<TaskCompletionSource> interTaskWaitReceive;
        private readonly Task taskReceive;
        private readonly TicketLock lockSend;

        public PlayerSetting Setting => PlayerManager.ServerEngine.Setting.Player;
        public EndPoint? LocalEndPoint => socketClient?.LocalEndPoint;
        public EndPoint? RemoteEndPoint => socketClient?.RemoteEndPoint;
        public ITime Time => PlayerManager.ServerEngine.Time;
        public bool IsConnect => isConnect.Value;
        public bool IsWorking => !taskReceive.IsCompleted || lockSend.IsLock;
        #endregion

        #region Construction
        internal PlayerControl(PlayerManager playerManager, Socket socketClient, IQueueIn<IEvent> queueReceive, IQueueIn<IEvent> queueSend, ILogHander logHander)
        {
            PlayerManager = playerManager;
            this.socketClient = socketClient;
            this.queueReceive = queueReceive;
            this.queueSend = queueSend;
            this.logHander = logHander;
            Name = GetPlayerName(socketClient);
            Setting.Socket.ApplyTo(socketClient);
            cancellationTokenSource = new();
            isConnect = new(true);
            requestMessagePerFrame = Setting.GetRequestMessagePerFrame();
            requestMessage = new();
            interTaskWaitReceive = new();
            lockSend = new();
            //
            if (!Init_PlayerHander(out playerHander, out taskReceive))
            {
                this.logHander.LogWarning(LOG_CRATE_PLAYER_HANDER_FAIL);
                //
                Close();
            }
        }
        private bool Init_PlayerHander(out IPlayerHander playerHander, out Task taskReceive)
        {
            if (!PlayerManager.ServerEngine.Hander_PlayerConnect(this, out IPlayerHander? tmpPlayerHander))
            {
                playerHander = PlayerHanderNone.Instance;
                taskReceive = Task.CompletedTask;
                return false;
            }
            //
            playerHander = tmpPlayerHander;
            //
            try
            {
                TaskCompletionSource? taskWaitAccept = interTaskWaitReceive.Exchange(new());
                taskWaitAccept?.TrySetResult();
                taskReceive = Task.Run(ReceiveAsync);
                return true;
            }
            catch (Exception)
            {
                taskReceive = Task.CompletedTask;
                return false;
            }
        }
        #endregion

        #region Method
        private static string GetPlayerName(Socket socket)
        {
            if (SocketUtilities.TryGetText(socket.LocalEndPoint, socket.RemoteEndPoint, out string text))
                return string.Format(NAME_FORMAT, text);
            else
                return NAME_DEFAULT;
        }
        public override string ToString()
        {
            return Name;
        }
        #endregion

        #region Engine
        internal void Start()
        {
            if (requestMessage.Exchange(requestMessagePerFrame) <= 0)
            {
                TaskCompletionSource? taskWaitAccept = interTaskWaitReceive.Exchange(new());
                taskWaitAccept?.TrySetResult();
            }
            Hander_Start();
        }
        internal void Update()
        {
            if (requestMessage.Exchange(requestMessagePerFrame) <= 0)
            {
                TaskCompletionSource? taskWaitAccept = interTaskWaitReceive.Exchange(new());
                taskWaitAccept?.TrySetResult();
            }
            Hander_Update();
        }
        internal void Stop()
        {
            cancellationTokenSource.Dispose();
            socketClient.Close();
            queueSend.Disable();
            queueReceive.Disable();
            //
            Hander_Stop();
            playerHander = PlayerHanderNone.Instance;
            //
            logHander.DisableLog();
        }
        #endregion

        #region Close
        public void Close()
        {
            if (!isConnect.TryExchange(false))
                return;
            //
            try
            {
                cancellationTokenSource.Cancel();
            }
            catch (Exception)
            {

            }
            //
            try
            {
                socketClient.Shutdown(SocketShutdown.Both);
            }
            catch (Exception ex)
            {
                logHander.LogWarning(Name, LOG_SOCKET_CLOSE_FAIL, ex.Message);
            }
            //
            TaskCompletionSource? taskWaitAccept = interTaskWaitReceive.Exchange(null);
            taskWaitAccept?.TrySetResult();
        }
        #endregion

        #region Hander
        private void Hander_Start()
        {
            try
            {
                playerHander.Player_OnStart(this);
            }
            catch (Exception ex)
            {
                logHander.LogWarning(Name, LOG_CALL_ON_START_FAIL, ex.Message);
            }
        }
        private void Hander_Update()
        {
            try
            {
                playerHander.Player_OnUpdate(this);
            }
            catch (Exception ex)
            {
                logHander.LogWarning(Name, LOG_CALL_ON_UPDATE_FAIL, ex.Message);
            }
        }
        private void Hander_Stop()
        {
            try
            {
                playerHander.Player_OnStop(this);
            }
            catch (Exception ex)
            {
                logHander.LogWarning(Name, LOG_CALL_ON_STOP_FAIL, ex.Message);
            }
        }
        private void Hander_Disconnect(SocketClose close)
        {
            try
            {
                playerHander.Player_OnDisconnect(this, close);
            }
            catch (Exception ex)
            {
                logHander.LogWarning(Name, LOG_CALL_ON_DISCONNECT_FAIL, ex.Message);
            }
        }
        private void Hander_Receive(byte[] message)
        {
            try
            {
                playerHander.Player_OnReceive(this, message);
            }
            catch (Exception ex)
            {
                logHander.LogWarning(Name, LOG_CALL_ON_RECEIVE_FAIL, ex.Message);
            }
        }
        private void Hander_Send(ResultSend result, SocketUtilities.OnSend onSend)
        {
            try
            {
                onSend(result);
            }
            catch (Exception ex)
            {
                string message = string.Format(ERROR_CALL_ON_SEND_FAIL, onSend.Method.Name, ex.Message);
                logHander.LogWarning(Name, LOG_CALL_ON_SEND_FAIL, message);
            }
        }
        private void Hander_Send<T>(ResultSend result, SocketUtilities.OnSend<T> onSend, T state)
        {
            try
            {
                onSend(result, state);
            }
            catch (Exception ex)
            {
                string title = string.Format(LOG_CALL_ON_SEND_T_FAIL, typeof(T).Name),
                    message = string.Format(ERROR_CALL_ON_SEND_FAIL, onSend.Method.Name, ex.Message);
                logHander.LogWarning(Name, title, message);
            }
        }
        private void Hander_Send<T1, T2>(ResultSend result, SocketUtilities.OnSend<T1, T2> onSend, T1 state1, T2 state2)
        {
            try
            {
                onSend(result, state1, state2);
            }
            catch (Exception ex)
            {
                string title = string.Format(LOG_CALL_ON_SEND_T1_T2_FAIL, typeof(T1).Name, typeof(T2).Name),
                    message = string.Format(ERROR_CALL_ON_SEND_FAIL, onSend.Method.Name, ex.Message);
                logHander.LogWarning(Name, title, message);
            }
        }
        private void Hander_Send<T1, T2, T3>(ResultSend result, SocketUtilities.OnSend<T1, T2, T3> onSend, T1 state1, T2 state2, T3 state3)
        {
            try
            {
                onSend(result, state1, state2, state3);
            }
            catch (Exception ex)
            {
                string title = string.Format(LOG_CALL_ON_SEND_T1_T2_T3_FAIL, typeof(T1).Name, typeof(T2).Name, typeof(T3).Name),
                    message = string.Format(ERROR_CALL_ON_SEND_FAIL, onSend.Method.Name, ex.Message);
                logHander.LogWarning(Name, title, message);
            }
        }
        private void Hander_Send<T1, T2, T3, T4>(ResultSend result, SocketUtilities.OnSend<T1, T2, T3, T4> onSend, T1 state1, T2 state2, T3 state3, T4 state4)
        {
            try
            {
                onSend(result, state1, state2, state3, state4);
            }
            catch (Exception ex)
            {
                string title = string.Format(LOG_CALL_ON_SEND_T1_T2_T3_T4_FAIL, typeof(T1).Name, typeof(T2).Name, typeof(T3).Name, typeof(T4).Name),
                    message = string.Format(ERROR_CALL_ON_SEND_FAIL, onSend.Method.Name, ex.Message);
                logHander.LogWarning(Name, title, message);
            }
        }
        #endregion

        #region Send
        public void Send(ReadOnlyMemory<byte> message, SocketUtilities.OnSend? onSend = null)
        {
            if (!IsConnect)
            {
                onSend?.Invoke(ResultSend.Fail(ERROR_SEND_NOT_CONNET_CLIENT_FAIL, SocketError.NotConnected));
                return;
            }
            //
            _ = lockSend.WaitAsync().ContinueWith(async (task) =>
            {
                ResultSend result;
                if (IsConnect)
                    result = await SocketUtilities.SendMessageAsync(socketClient, message, cancellationTokenSource.Token);
                else
                    result = ResultSend.Fail(ERROR_SEND_NOT_CONNET_CLIENT_FAIL, SocketError.NotConnected);
                //
                if (onSend != null)
                    _ = await queueSend.TryEnqueueAsync(new EventSend(Hander_Send, result, onSend));
                //
                task.Result.Release();
            });
        }
        public void Send<T>(ReadOnlyMemory<byte> message, SocketUtilities.OnSend<T>? onSend, T state)
        {
            if (!IsConnect)
            {
                onSend?.Invoke(ResultSend.Fail(ERROR_SEND_NOT_CONNET_CLIENT_FAIL, SocketError.NotConnected), state);
                return;
            }
            //
            _ = lockSend.WaitAsync().ContinueWith(async (task) =>
            {
                ResultSend result;
                if (IsConnect)
                    result = await SocketUtilities.SendMessageAsync(socketClient, message, cancellationTokenSource.Token);
                else
                    result = ResultSend.Fail(ERROR_SEND_NOT_CONNET_CLIENT_FAIL, SocketError.NotConnected);
                //
                if (onSend != null)
                    _ = await queueSend.TryEnqueueAsync(new EventSend<T>(Hander_Send, result, onSend, state));
                //
                task.Result.Release();
            });
        }
        public void Send<T1, T2>(ReadOnlyMemory<byte> message, SocketUtilities.OnSend<T1, T2>? onSend, T1 state1, T2 state2)
        {
            if (!IsConnect)
            {
                onSend?.Invoke(ResultSend.Fail(ERROR_SEND_NOT_CONNET_CLIENT_FAIL, SocketError.NotConnected), state1, state2);
                return;
            }
            //
            _ = lockSend.WaitAsync().ContinueWith(async (task) =>
            {
                ResultSend result;
                if (IsConnect)
                    result = await SocketUtilities.SendMessageAsync(socketClient, message, cancellationTokenSource.Token);
                else
                    result = ResultSend.Fail(ERROR_SEND_NOT_CONNET_CLIENT_FAIL, SocketError.NotConnected);
                //
                if (onSend != null)
                    _ = await queueSend.TryEnqueueAsync(new EventSend<T1, T2>(Hander_Send, result, onSend, state1, state2));
                //
                task.Result.Release();
            });
        }
        public void Send<T1, T2, T3>(ReadOnlyMemory<byte> message, SocketUtilities.OnSend<T1, T2, T3>? onSend, T1 state1, T2 state2, T3 state3)
        {
            if (!IsConnect)
            {
                onSend?.Invoke(ResultSend.Fail(ERROR_SEND_NOT_CONNET_CLIENT_FAIL, SocketError.NotConnected), state1, state2, state3);
                return;
            }
            //
            _ = lockSend.WaitAsync().ContinueWith(async (task) =>
            {
                ResultSend result;
                if (IsConnect)
                    result = await SocketUtilities.SendMessageAsync(socketClient, message, cancellationTokenSource.Token);
                else
                    result = ResultSend.Fail(ERROR_SEND_NOT_CONNET_CLIENT_FAIL, SocketError.NotConnected);
                //
                if (onSend != null)
                    _ = await queueSend.TryEnqueueAsync(new EventSend<T1, T2, T3>(Hander_Send, result, onSend, state1, state2, state3));
                //
                task.Result.Release();
            });
        }
        public void Send<T1, T2, T3, T4>(ReadOnlyMemory<byte> message, SocketUtilities.OnSend<T1, T2, T3, T4>? onSend, T1 state1, T2 state2, T3 state3, T4 state4)
        {
            if (!IsConnect)
            {
                onSend?.Invoke(ResultSend.Fail(ERROR_SEND_NOT_CONNET_CLIENT_FAIL, SocketError.NotConnected), state1, state2, state3, state4);
                return;
            }
            //
            _ = lockSend.WaitAsync().ContinueWith(async (task) =>
            {
                ResultSend result;
                if (IsConnect)
                    result = await SocketUtilities.SendMessageAsync(socketClient, message, cancellationTokenSource.Token);
                else
                    result = ResultSend.Fail(ERROR_SEND_NOT_CONNET_CLIENT_FAIL, SocketError.NotConnected);
                //
                if (onSend != null)
                    _ = await queueSend.TryEnqueueAsync(new EventSend<T1, T2, T3, T4>(Hander_Send, result, onSend, state1, state2, state3, state4));
                //
                task.Result.Release();
            });
        }
        #endregion

        #region Receive
        private async Task ReceiveAsync()
        {
            TaskCompletionSource? taskWaitReceive = interTaskWaitReceive.Value;
            while (IsConnect)
            {
                if (requestMessage.Value <= 0)
                {
                    if (taskWaitReceive == null)
                        break;
                    else
                        await taskWaitReceive.Task;
                    taskWaitReceive = interTaskWaitReceive.Value;
                    continue;
                }
                //
                ResultReceive result = await SocketUtilities.ReceiveMessageAsync(socketClient, cancellationTokenSource.Token);
                if (result.IsSuccess)
                {
                    if (await queueReceive.TryEnqueueAsync(new EventReceive(Hander_Receive, result.Data)))
                    {
                        requestMessage.Decrement();
                    }
                }
                else
                {
                    Close();
                    if (result.ErrorCode == SocketError.ConnectionReset)
                        _ = await queueReceive.TryEnqueueAsync(new EventDisconnect(Hander_Disconnect, SocketClose.Remote));
                    else
                        _ = await queueReceive.TryEnqueueAsync(new EventDisconnect(Hander_Disconnect, SocketClose.Local));
                    break;
                }
            }
        }
        #endregion
    }
}
