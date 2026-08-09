using KemLibCore;
using KemLibCore.Concurrent.Inter;
using KemLibCore.Concurrent.Locker;
using System.Net.Sockets;

namespace KemNetEngine.Network
{
    public class Listener : IDisposable
    {
        #region Properties
        protected const string ERROR_LISTENER_HAS_BEEN_DISPOSED = "the Listener has been Disposed",
            ERROR_LISTENER_IS_NOT_RUN = "Listener is not run",
            ERROR_LISTENER_IS_RUN = "Listener is run",
            ERROR_OPERATION_CANCELED_EXCEPTION = "CancellationToken was canceled";
        private const string NAME_DEFAULT = "Listener",
            NAME_FORMAT = "Listener({0})";

        public readonly ListenSetting Setting;
        private readonly SocketSetting socketSetting;
        private readonly TicketLock lockListen;
        private InterObjectStruct<Socket> interSocket;
        private InterBoolStruct interRun,
            interDisposed,
            interBlocking;
        private readonly TicketLock lockAccept;

        public bool IsRun => interRun.Value;
        public bool IsDisposed => interDisposed.Value;
        public bool Blocking
        {
            get => interBlocking.Value;
            set => interBlocking.Exchange(value);
        }
        #endregion

        #region Constructions
        public Listener(ListenSetting setting, SocketSetting socketSetting)
        {
            Setting = setting;
            this.socketSetting = socketSetting;
            lockListen = new();
            interSocket = new();
            interRun = new();
            interDisposed = new();
            interBlocking = new();
            lockAccept = new();
        }
        ~Listener()
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
            Stop();
            if (disposing)
            {

            }
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            if (SocketUtilities.TryGetText(Setting.EndPoint, null, out string text))
                return string.Format(NAME_FORMAT, text);
            else
                return NAME_DEFAULT;
        }
        #endregion

        #region Listen
        public Result Start()
        {
            if (IsDisposed)
                return Result.Fail(ERROR_LISTENER_HAS_BEEN_DISPOSED);
            if (IsRun)
                return Result.Fail(ERROR_LISTENER_IS_RUN);
            //
            Ticket ticket = lockListen.Wait();
            Socket? socket = null;
            try
            {
                if (IsDisposed)
                    return Result.Fail(ERROR_LISTENER_HAS_BEEN_DISPOSED);
                if (IsRun)
                    return Result.Fail(ERROR_LISTENER_IS_RUN);
                //
                socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                {
                    Blocking = interBlocking.Value
                };
                socket.Bind(Setting.EndPoint);
                socket.Listen(Setting.BacklogCapacity);
                //
                interSocket.Exchange(socket);
                interRun.Value = true;
                //
                return Result.Success();
            }
            catch (Exception ex)
            {
                SocketUtilities.Socket_Close(socket);
                return Result.Fail(ex.Message);
            }
            finally
            {
                ticket.Release();
            }
        }
        public async Task<Result> StartAsync()
        {
            if (IsDisposed)
                return Result.Fail(ERROR_LISTENER_HAS_BEEN_DISPOSED);
            if (IsRun)
                return Result.Fail(ERROR_LISTENER_IS_RUN);
            //
            Ticket ticket = await lockListen.WaitAsync();
            Socket? socket = null;
            try
            {
                if (IsDisposed)
                    return Result.Fail(ERROR_LISTENER_HAS_BEEN_DISPOSED);
                if (IsRun)
                    return Result.Fail(ERROR_LISTENER_IS_RUN);
                //
                socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                {
                    Blocking = interBlocking.Value
                };
                socket.Bind(Setting.EndPoint);
                socket.Listen(Setting.BacklogCapacity);
                //
                interSocket.Exchange(socket);
                interRun.Value = true;
                //
                return Result.Success();
            }
            catch (Exception ex)
            {
                SocketUtilities.Socket_Close(socket);
                return Result.Fail(ex.Message);
            }
            finally
            {
                ticket.Release();
            }
        }
        public void Stop()
        {
            if (IsDisposed)
                return;
            if (!IsRun)
                return;
            //
            Ticket ticket = lockListen.Wait();
            try
            {
                if (IsDisposed)
                    return;
                if (!IsRun)
                    return;
                //
                Socket? socket = interSocket.Exchange(null);
                SocketUtilities.Socket_Close(socket);
                //
                interRun.Value = false;
            }
            catch (Exception)
            {

            }
            finally
            {
                ticket.Release();
            }
        }
        public async Task StopAsync()
        {
            if (IsDisposed)
                return;
            if (!IsRun)
                return;
            //
            Ticket ticket = await lockListen.WaitAsync();
            try
            {
                if (IsDisposed)
                    return;
                if (!IsRun)
                    return;
                //
                Socket? socket = interSocket.Exchange(null);
                SocketUtilities.Socket_Close(socket);
                //
                interRun.Value = false;
            }
            catch (Exception)
            {

            }
            finally
            {
                ticket.Release();
            }
        }
        #endregion

        #region Accept
        public ResultConnect Accept()
        {
            if (IsDisposed)
                return ResultConnect.Fail(ERROR_LISTENER_HAS_BEEN_DISPOSED, SocketError.SocketError);
            if (!IsRun)
                return ResultConnect.Fail(ERROR_LISTENER_IS_NOT_RUN, SocketError.Shutdown);
            //
            Ticket ticket = lockAccept.Wait();
            try
            {
                if (IsDisposed)
                    return ResultConnect.Fail(ERROR_LISTENER_HAS_BEEN_DISPOSED, SocketError.SocketError);
                if (!IsRun)
                    return ResultConnect.Fail(ERROR_LISTENER_IS_NOT_RUN, SocketError.SocketError);
                //
                Socket? socket = interSocket.Value;
                if (socket == null)
                    return ResultConnect.Fail(ERROR_LISTENER_IS_NOT_RUN, SocketError.SocketError);
                //
                socket.Blocking = Blocking;
                Socket socketClient = socket.Accept();
                //
                ClientControl client = new(socketClient, socketSetting);
                return ResultConnect.Success(client);
            }
            catch (SocketException ex)
            {
                return ResultConnect.Fail(ex.Message, ex.SocketErrorCode);
            }
            catch (Exception ex)
            {
                return ResultConnect.Fail(ex.Message, SocketError.SocketError);
            }
            finally
            {
                ticket.Release();
            }
        }
        public async Task<ResultConnect> AcceptAsync(CancellationToken cancellationToken = default)
        {
            if (IsDisposed)
                return ResultConnect.Fail(ERROR_LISTENER_HAS_BEEN_DISPOSED, SocketError.SocketError);
            if (!IsRun)
                return ResultConnect.Fail(ERROR_LISTENER_IS_NOT_RUN, SocketError.Shutdown);
            //
            TicketAccept ticket = await lockAccept.WaitAsync(cancellationToken);
            if (!ticket.IsAccept)
                return ResultConnect.Fail(ERROR_OPERATION_CANCELED_EXCEPTION, SocketError.SocketError);
            //
            try
            {
                if (IsDisposed)
                    return ResultConnect.Fail(ERROR_LISTENER_HAS_BEEN_DISPOSED, SocketError.SocketError);
                if (!IsRun)
                    return ResultConnect.Fail(ERROR_LISTENER_IS_NOT_RUN, SocketError.SocketError);
                //
                Socket? socket = interSocket.Value;
                if (socket == null)
                    return ResultConnect.Fail(ERROR_LISTENER_IS_NOT_RUN, SocketError.SocketError);
                //
                Socket socketClient = await socket.AcceptAsync(cancellationToken);
                //
                ClientControl client = new(socketClient, socketSetting);
                return ResultConnect.Success(client);
            }
            catch (OperationCanceledException)
            {
                return ResultConnect.Fail(ERROR_OPERATION_CANCELED_EXCEPTION, SocketError.SocketError);
            }
            catch (SocketException ex)
            {
                return ResultConnect.Fail(ex.Message, ex.SocketErrorCode);
            }
            catch (Exception ex)
            {
                return ResultConnect.Fail(ex.Message, SocketError.SocketError);
            }
            finally
            {
                ticket.Release();
            }
        }
        public void Accept(SocketUtilities.OnConnect? callback, CancellationToken cancellationToken = default)
        {
            _ = AcceptAsync(cancellationToken).ContinueWith((task) =>
            {
                callback?.Invoke(task.Result);
            }, CancellationToken.None);
        }
        public void Accept<T>(SocketUtilities.OnConnect<T>? callback, T state, CancellationToken cancellationToken = default)
        {
            _ = AcceptAsync(cancellationToken).ContinueWith((task) =>
            {
                callback?.Invoke(task.Result, state);
            }, CancellationToken.None);
        }
        public void Accept<T1, T2>(SocketUtilities.OnConnect<T1, T2>? callback, T1 state1, T2 state2, CancellationToken cancellationToken = default)
        {
            _ = AcceptAsync(cancellationToken).ContinueWith((task) =>
            {
                callback?.Invoke(task.Result, state1, state2);
            }, CancellationToken.None);
        }
        public void Accept<T1, T2, T3>(SocketUtilities.OnConnect<T1, T2, T3>? callback, T1 state1, T2 state2, T3 state3, CancellationToken cancellationToken = default)
        {
            _ = AcceptAsync(cancellationToken).ContinueWith((task) =>
            {
                callback?.Invoke(task.Result, state1, state2, state3);
            }, CancellationToken.None);
        }
        public void Accept<T1, T2, T3, T4>(SocketUtilities.OnConnect<T1, T2, T3, T4>? callback, T1 state1, T2 state2, T3 state3, T4 state4, CancellationToken cancellationToken = default)
        {
            _ = AcceptAsync(cancellationToken).ContinueWith((task) =>
            {
                callback?.Invoke(task.Result, state1, state2, state3, state4);
            }, CancellationToken.None);
        }
        #endregion
    }
}
