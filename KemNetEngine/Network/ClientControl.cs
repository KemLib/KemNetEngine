using KemLibCore.Concurrent.Inter;
using KemLibCore.Concurrent.Locker;
using System.Net;
using System.Net.Sockets;

namespace KemNetEngine.Network
{
    public class ClientControl : IClient
    {
        #region Properties
        protected const string ERROR_CLIENT_HAS_BEEN_DISPOSED = "the Client has been Disposed",
            ERROR_SOCKET_IS_NOT_CONNECTED = "the Socket is not connected",
            ERROR_OPERATION_CANCELED_EXCEPTION = "CancellationToken was canceled";
        private const string NAME_DEFAULT = "Client",
            NAME_FORMAT = "Client({0})";

        public readonly SocketSetting Setting;
        private readonly Socket? socket;
        private readonly string name;
        private InterBoolStruct interConnect,
            interDisposed,
            interCloseFrom;
        private InterLock lockEventDisconnect;
        private readonly TicketLock lockSend,
            lockReceive;

        private event IClient.OnDisconnectDelegate? OnDisconnectTmp;
        public event IClient.OnDisconnectDelegate OnDisconnect
        {
            add
            {
                if (value == null)
                    return;
                //
                lockEventDisconnect.Enter();
                try
                {
                    OnDisconnectTmp += value;
                }
                catch (Exception)
                {

                }
                finally
                {
                    lockEventDisconnect.Exit();
                }
            }
            remove
            {
                if (value == null)
                    return;
                //
                lockEventDisconnect.Enter();
                try
                {
                    OnDisconnectTmp -= value;
                }
                catch (Exception)
                {

                }
                finally
                {
                    lockEventDisconnect.Exit();
                }
            }
        }

        public string Name => name;
        public EndPoint? LocalEndPoint => socket?.LocalEndPoint;
        public EndPoint? RemoteEndPoint => socket?.RemoteEndPoint;
        public bool IsConnect => interConnect.Value;
        public bool IsDisposed => interDisposed.Value;
        public SocketClose CloseFrom => interCloseFrom.Value ? SocketClose.Local : SocketClose.Remote;
        public bool IsSending => lockSend.IsLock;
        public bool IsReceiving => lockReceive.IsLock;
        public int DataAvailable
        {
            get
            {
                if (socket == null)
                    return 0;
                try
                {
                    return socket.Available;
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.ConnectionReset)
                        Close(SocketClose.Remote);
                    else
                        Close(SocketClose.Local);
                    return -1;
                }
                catch (Exception)
                {
                    Close(SocketClose.Local);
                    return -1;
                }
            }
        }
        #endregion

        #region Constructions
        public ClientControl(Socket socket, SocketSetting setting)
        {
            this.socket = socket;
            name = GetName(socket.LocalEndPoint, socket.RemoteEndPoint);
            interConnect = new(true);
            setting.ApplyTo(socket);
            Setting = setting;
            interDisposed = new();
            interCloseFrom = new();
            lockEventDisconnect = new();
            lockSend = new();
            lockReceive = new();
        }
        ~ClientControl()
        {
            Dispose(false);
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!interDisposed.TryExchange(true))
                return;
            //
            Close();
            if (disposing)
            {

            }
        }
        #endregion

        #region Methods
        private static string GetName(EndPoint? localEndPoint, EndPoint? remoteEndPoint)
        {
            if (SocketUtilities.TryGetText(localEndPoint, remoteEndPoint, out string text))
                return string.Format(NAME_FORMAT, text);
            else
                return NAME_DEFAULT;
        }
        public override string ToString()
        {
            return name;
        }
        #endregion

        #region Close
        public void Close()
        {
            Close(SocketClose.Local);
        }
        private void Close(SocketClose close)
        {
            if (!interConnect.TryExchange(false))
                return;
            //
            interCloseFrom.Exchange(close == SocketClose.Local);
            SocketUtilities.Socket_Close(socket);
            //
            try
            {
                _ = Task.Run(PushDisconnect);
            }
            catch (Exception)
            {

            }
        }
        private void PushDisconnect()
        {
            lockEventDisconnect.Enter();
            try
            {
                OnDisconnectTmp?.Invoke(this);
            }
            catch (Exception)
            {

            }
            finally
            {
                lockEventDisconnect.Exit();
            }
        }
        #endregion

        #region Send
        public ResultSend Send(ReadOnlyMemory<byte> message)
        {
            if (!IsConnect)
            {
                if (IsDisposed)
                    return ResultSend.Fail(ERROR_CLIENT_HAS_BEEN_DISPOSED, SocketError.NotConnected);
                return ResultSend.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected);
            }
            //
            Ticket ticket = lockSend.Wait();
            try
            {
                if (IsConnect)
                {
                    if (socket == null)
                        return ResultSend.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected);
                    else
                        return SocketUtilities.SendMessage(socket, message);
                }
                else
                {
                    if (IsDisposed)
                        return ResultSend.Fail(ERROR_CLIENT_HAS_BEEN_DISPOSED, SocketError.NotConnected);
                    return ResultSend.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected);
                }
            }
            catch (Exception ex)
            {
                return ResultSend.Fail(ex.Message, SocketError.SocketError);
            }
            finally
            {
                ticket.Release();
            }
        }
        public async Task<ResultSend> SendAsync(ReadOnlyMemory<byte> message, CancellationToken cancellationToken = default)
        {
            if (!IsConnect)
            {
                if (IsDisposed)
                    return ResultSend.Fail(ERROR_CLIENT_HAS_BEEN_DISPOSED, SocketError.NotConnected);
                return ResultSend.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected);
            }
            //
            TicketAccept ticket = await lockSend.WaitAsync(cancellationToken);
            if (!ticket.IsAccept)
                return ResultSend.Fail(ERROR_OPERATION_CANCELED_EXCEPTION, SocketError.SocketError);
            //
            try
            {
                if (IsConnect)
                {
                    if (socket == null)
                        return ResultSend.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected);
                    else
                        return await SocketUtilities.SendMessageAsync(socket, message, cancellationToken);
                }
                else
                {
                    if (IsDisposed)
                        return ResultSend.Fail(ERROR_CLIENT_HAS_BEEN_DISPOSED, SocketError.NotConnected);
                    return ResultSend.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected);
                }
            }
            catch (Exception ex)
            {
                return ResultSend.Fail(ex.Message, SocketError.SocketError);
            }
            finally
            {
                ticket.Release();
            }
        }
        public void Send(ReadOnlyMemory<byte> message, SocketUtilities.OnSend? callback, CancellationToken cancellationToken = default)
        {
            if (!IsConnect)
            {
                if (IsDisposed)
                    callback?.Invoke(ResultSend.Fail(ERROR_CLIENT_HAS_BEEN_DISPOSED, SocketError.NotConnected));
                callback?.Invoke(ResultSend.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected));
                return;
            }
            //
            _ = SendAsync(message, cancellationToken).ContinueWith(async (task) =>
            {
                callback?.Invoke(task.Result);
            }, CancellationToken.None);
        }
        public void Send<T>(ReadOnlyMemory<byte> message, SocketUtilities.OnSend<T>? callback, T state, CancellationToken cancellationToken = default)
        {
            if (!IsConnect)
            {
                if (IsDisposed)
                    callback?.Invoke(ResultSend.Fail(ERROR_CLIENT_HAS_BEEN_DISPOSED, SocketError.NotConnected), state);
                callback?.Invoke(ResultSend.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected), state);
                return;
            }
            //
            _ = SendAsync(message, cancellationToken).ContinueWith(async (task) =>
            {
                callback?.Invoke(task.Result, state);
            }, CancellationToken.None);
        }
        public void Send<T1, T2>(ReadOnlyMemory<byte> message, SocketUtilities.OnSend<T1, T2>? callback, T1 state1, T2 state2, CancellationToken cancellationToken = default)
        {
            if (!IsConnect)
            {
                if (IsDisposed)
                    callback?.Invoke(ResultSend.Fail(ERROR_CLIENT_HAS_BEEN_DISPOSED, SocketError.NotConnected), state1, state2);
                callback?.Invoke(ResultSend.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected), state1, state2);
                return;
            }
            //
            _ = SendAsync(message, cancellationToken).ContinueWith(async (task) =>
            {
                callback?.Invoke(task.Result, state1, state2);
            }, CancellationToken.None);
        }
        public void Send<T1, T2, T3>(ReadOnlyMemory<byte> message, SocketUtilities.OnSend<T1, T2, T3>? callback, T1 state1, T2 state2, T3 state3, CancellationToken cancellationToken = default)
        {
            if (!IsConnect)
            {
                if (IsDisposed)
                    callback?.Invoke(ResultSend.Fail(ERROR_CLIENT_HAS_BEEN_DISPOSED, SocketError.NotConnected), state1, state2, state3);
                callback?.Invoke(ResultSend.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected), state1, state2, state3);
                return;
            }
            //
            _ = SendAsync(message, cancellationToken).ContinueWith(async (task) =>
            {
                callback?.Invoke(task.Result, state1, state2, state3);
            }, CancellationToken.None);
        }
        public void Send<T1, T2, T3, T4>(ReadOnlyMemory<byte> message, SocketUtilities.OnSend<T1, T2, T3, T4>? callback, T1 state1, T2 state2, T3 state3, T4 state4, CancellationToken cancellationToken = default)
        {
            if (!IsConnect)
            {
                if (IsDisposed)
                    callback?.Invoke(ResultSend.Fail(ERROR_CLIENT_HAS_BEEN_DISPOSED, SocketError.NotConnected), state1, state2, state3, state4);
                callback?.Invoke(ResultSend.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected), state1, state2, state3, state4);
                return;
            }
            //
            _ = SendAsync(message, cancellationToken).ContinueWith(async (task) =>
            {
                callback?.Invoke(task.Result, state1, state2, state3, state4);
            }, CancellationToken.None);
        }
        #endregion

        #region Receive
        public ResultReceive Receive()
        {
            if (!IsConnect)
            {
                if (IsDisposed)
                    return ResultReceive.Fail(ERROR_CLIENT_HAS_BEEN_DISPOSED, SocketError.NotConnected);
                return ResultReceive.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected);
            }
            //
            Ticket ticket = lockReceive.Wait();
            try
            {
                if (IsConnect)
                {
                    if (socket == null)
                    {
                        return ResultReceive.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected);
                    }
                    else
                    {
                        ResultReceive result = SocketUtilities.ReceiveMessage(socket);
                        if (!result.IsSuccess)
                        {
                            if (result.ErrorCode == SocketError.ConnectionReset)
                                Close(SocketClose.Remote);
                            else
                                Close(SocketClose.Local);
                        }
                        return result;
                    }
                }
                else
                {
                    if (IsDisposed)
                        return ResultReceive.Fail(ERROR_CLIENT_HAS_BEEN_DISPOSED, SocketError.NotConnected);
                    return ResultReceive.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected);
                }
            }
            catch (Exception ex)
            {
                return ResultReceive.Fail(ex.Message, SocketError.SocketError);
            }
            finally
            {
                ticket.Release();
            }
        }
        public async Task<ResultReceive> ReceiveAsync(CancellationToken cancellationToken = default)
        {
            if (!IsConnect)
            {
                if (IsDisposed)
                    return ResultReceive.Fail(ERROR_CLIENT_HAS_BEEN_DISPOSED, SocketError.NotConnected);
                return ResultReceive.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected);
            }
            //
            TicketAccept ticket = await lockReceive.WaitAsync(cancellationToken);
            if (!ticket.IsAccept)
                return ResultReceive.Fail(ERROR_OPERATION_CANCELED_EXCEPTION, SocketError.SocketError);
            //
            try
            {
                if (IsConnect)
                {
                    if (socket == null)
                    {
                        return ResultReceive.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected);
                    }
                    else
                    {
                        ResultReceive result = await SocketUtilities.ReceiveMessageAsync(socket, cancellationToken);
                        if (!result.IsSuccess)
                        {
                            if (result.ErrorCode == SocketError.ConnectionReset)
                                Close(SocketClose.Remote);
                            else
                                Close(SocketClose.Local);
                        }
                        return result;
                    }
                }
                else
                {
                    if (IsDisposed)
                        return ResultReceive.Fail(ERROR_CLIENT_HAS_BEEN_DISPOSED, SocketError.NotConnected);
                    return ResultReceive.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected);
                }
            }
            catch (Exception ex)
            {
                return ResultReceive.Fail(ex.Message, SocketError.SocketError);
            }
            finally
            {
                ticket.Release();
            }
        }
        public void Receive(SocketUtilities.OnReceive? callback, CancellationToken cancellationToken = default)
        {
            if (!IsConnect)
            {
                if (IsDisposed)
                    callback?.Invoke(ResultReceive.Fail(ERROR_CLIENT_HAS_BEEN_DISPOSED, SocketError.NotConnected));
                callback?.Invoke(ResultReceive.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected));
                return;
            }
            //
            _ = ReceiveAsync(cancellationToken).ContinueWith(async (task) =>
            {
                callback?.Invoke(task.Result);
            }, CancellationToken.None);
        }
        public void Receive<T>(SocketUtilities.OnReceive<T>? callback, T state, CancellationToken cancellationToken = default)
        {
            if (!IsConnect)
            {
                if (IsDisposed)
                    callback?.Invoke(ResultReceive.Fail(ERROR_CLIENT_HAS_BEEN_DISPOSED, SocketError.NotConnected), state);
                callback?.Invoke(ResultReceive.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected), state);
                return;
            }
            //
            _ = ReceiveAsync(cancellationToken).ContinueWith(async (task) =>
            {
                callback?.Invoke(task.Result, state);
            }, CancellationToken.None);
        }
        public void Receive<T1, T2>(SocketUtilities.OnReceive<T1, T2>? callback, T1 state1, T2 state2, CancellationToken cancellationToken = default)
        {
            if (!IsConnect)
            {
                if (IsDisposed)
                    callback?.Invoke(ResultReceive.Fail(ERROR_CLIENT_HAS_BEEN_DISPOSED, SocketError.NotConnected), state1, state2);
                callback?.Invoke(ResultReceive.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected), state1, state2);
                return;
            }
            //
            _ = ReceiveAsync(cancellationToken).ContinueWith(async (task) =>
            {
                callback?.Invoke(task.Result, state1, state2);
            }, CancellationToken.None);
        }
        public void Receive<T1, T2, T3>(SocketUtilities.OnReceive<T1, T2, T3>? callback, T1 state1, T2 state2, T3 state3, CancellationToken cancellationToken = default)
        {
            if (!IsConnect)
            {
                if (IsDisposed)
                    callback?.Invoke(ResultReceive.Fail(ERROR_CLIENT_HAS_BEEN_DISPOSED, SocketError.NotConnected), state1, state2, state3);
                callback?.Invoke(ResultReceive.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected), state1, state2, state3);
                return;
            }
            //
            _ = ReceiveAsync(cancellationToken).ContinueWith(async (task) =>
            {
                callback?.Invoke(task.Result, state1, state2, state3);
            }, CancellationToken.None);
        }
        public void Receive<T1, T2, T3, T4>(SocketUtilities.OnReceive<T1, T2, T3, T4>? callback, T1 state1, T2 state2, T3 state3, T4 state4, CancellationToken cancellationToken = default)
        {
            if (!IsConnect)
            {
                if (IsDisposed)
                    callback?.Invoke(ResultReceive.Fail(ERROR_CLIENT_HAS_BEEN_DISPOSED, SocketError.NotConnected), state1, state2, state3, state4);
                callback?.Invoke(ResultReceive.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected), state1, state2, state3, state4);
                return;
            }
            //
            _ = ReceiveAsync(cancellationToken).ContinueWith(async (task) =>
            {
                callback?.Invoke(task.Result, state1, state2, state3, state4);
            }, CancellationToken.None);
        }
        #endregion
    }
}
