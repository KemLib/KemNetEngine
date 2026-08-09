using KemLibCore.Concurrent.Inter;
using KemLibCore.Concurrent.Queue;
using KemNetEngine.Network;
using System.Net.Sockets;

namespace KemNetEngine.Server
{
    internal class ListenManager
    {
        #region Properties
        private const string ERROR_LISTEN_IS_RUNNING = "Listen is running",
            ERROR_LISTEN_START_FAIL = "Listen start fail: ",
            ERROR_LISTEN_RUN_FAIL = "Listen run fail: ";

        public readonly ServerEngine ServerEngine;
        private readonly SingleQueue<Socket> queueClient;
        private InterObjectStruct<CancellationTokenSource> interCancelToken;
        private InterObjectStruct<Task> interTaskAccept;
        private InterObjectStruct<TaskCompletionSource> interTaskWaitAccept;
        private InterBoolStruct isRun;
        private InterIntStruct requestClient;

        public ListenSetting Setting => ServerEngine.Setting.Listen;
        public int RequestClient
        {
            get => requestClient.Value;
            set
            {
                if (value <= 0)
                    return;
                //
                if (requestClient.Exchange(value) <= 0)
                {
                    TaskCompletionSource? taskWaitAccept = interTaskWaitAccept.Exchange(new());
                    taskWaitAccept?.TrySetResult();
                }
            }
        }
        public bool IsRun => isRun.Value;
        #endregion

        #region Construction
        internal ListenManager(ServerEngine serverEngine)
        {
            ServerEngine = serverEngine;
            queueClient = new(Setting.BacklogCapacity);
            interCancelToken = new();
            interTaskAccept = new();
            interTaskWaitAccept = new();
            isRun = new();
            requestClient = new();
        }
        #endregion

        #region Engine
        internal bool TryStart([NotNullWhen(false)] out string? error)
        {
            if (!isRun.TryExchange(true))
            {
                error = ERROR_LISTEN_IS_RUNNING;
                return false;
            }
            //
            Socket? socketListener = null;
            try
            {
                socketListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socketListener.Bind(Setting.EndPoint);
                socketListener.Listen(Setting.BacklogCapacity);
            }
            catch (Exception ex)
            {
                socketListener?.Dispose();
                isRun.Value = false;
                //
                error = ERROR_LISTEN_START_FAIL + ex.Message;
                return false;
            }
            //
            try
            {
                CancellationTokenSource cancellationTokenSource = new();
                interCancelToken.Exchange(cancellationTokenSource);
                TaskCompletionSource? taskWaitAccept = interTaskWaitAccept.Exchange(new());
                taskWaitAccept?.TrySetResult();
                interTaskAccept.Value = Task.Run(async () =>
                {
                    await AcceptAsync(socketListener, cancellationTokenSource.Token);
                }, CancellationToken.None);
                //
                error = null;
                return true;
            }
            catch (Exception ex)
            {
                Stoping();
                socketListener.Close();
                isRun.Value = false;
                //
                error = ERROR_LISTEN_RUN_FAIL + ex.Message;
                return false;
            }
        }
        internal void Stoping()
        {
            queueClient.Disable();
            CancelTokenSource(interCancelToken.Exchange(null));
            TaskCompletionSource? taskWaitAccept = interTaskWaitAccept.Exchange(null);
            taskWaitAccept?.TrySetResult();
            //
            Client_Clear();
        }
        internal void Stop()
        {
            Stoping();
            //
            Task? taskAccept = interTaskAccept.Exchange(null);
            taskAccept?.Wait();
        }
        internal async Task StopingAsync()
        {
            queueClient.Disable();
            await CancelTokenSourceAsync(interCancelToken.Exchange(null));
            TaskCompletionSource? taskWaitAccept = interTaskWaitAccept.Exchange(null);
            taskWaitAccept?.TrySetResult();
            //
            await Client_ClearAsync();
        }
        internal async Task StopAsync()
        {
            await StopingAsync();
            //
            Task? taskAccept = interTaskAccept.Exchange(null);
            if (taskAccept != null)
                await taskAccept;
        }
        private static void CancelTokenSource(CancellationTokenSource? cancelToken)
        {
            if (cancelToken == null)
                return;
            //
            try
            {
                cancelToken.Cancel();
            }
            catch (Exception)
            {

            }
            finally
            {
                cancelToken.Dispose();
            }
        }
        private static async Task CancelTokenSourceAsync(CancellationTokenSource? cancelToken)
        {
            if (cancelToken == null)
                return;
            //
            try
            {
                await cancelToken.CancelAsync();
            }
            catch (Exception)
            {

            }
            finally
            {
                cancelToken.Dispose();
            }
        }
        #endregion

        #region Accept
        private async Task AcceptAsync(Socket socketListener, CancellationToken cancellationToken = default)
        {
            queueClient.Active();
            TaskCompletionSource? taskWaitAccept = interTaskWaitAccept.Value;
            while (!cancellationToken.IsCancellationRequested)
            {
                if (requestClient.Value <= 0)
                {
                    if (taskWaitAccept == null)
                        break;
                    else
                        await taskWaitAccept.Task;
                    taskWaitAccept = interTaskWaitAccept.Value;
                    continue;
                }
                //
                try
                {
                    Socket client = await socketListener.AcceptAsync(cancellationToken);
                    if (await queueClient.TryEnqueueAsync(client, cancellationToken))
                    {
                        requestClient.Decrement();
                    }
                    else
                    {
                        client.Close();
                    }
                }
                catch (Exception)
                {
                    break;
                }
            }
            //
            await StopingAsync();
            socketListener.Close();
            isRun.Value = false;
        }
        #endregion

        #region Client
        internal Socket[] Client_GetAll()
        {
            return queueClient.DequeueAll();
        }
        internal async Task<Socket[]> Client_GetAllAsync()
        {
            return await queueClient.DequeueAllAsync();
        }
        private void Client_Clear()
        {
            if (!queueClient.TryDequeueAll(out Socket[]? arrayClient))
                return;
            //
            foreach (var client in arrayClient)
            {
                client.Close();
            }
        }
        private async Task Client_ClearAsync()
        {
            Socket[] arrayClient = await queueClient.DequeueAllAsync();
            //
            foreach (var client in arrayClient)
            {
                client.Close();
            }
        }
        #endregion
    }
}
