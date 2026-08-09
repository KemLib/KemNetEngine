using System.Net.Sockets;

namespace KemNetEngine.Network
{
    public class Connecter
    {
        #region Properties
        private const string NAME_DEFAULT = "Connecter",
            NAME_FORMAT = "Connecter({0})";

        public readonly ConnectSetting Setting;
        private readonly SocketSetting socketSetting;
        #endregion

        #region Construction
        public Connecter(ConnectSetting setting, SocketSetting socketSetting)
        {
            Setting = setting;
            this.socketSetting = socketSetting;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            if (SocketUtilities.TryGetText(Setting.LocalEndPoint, Setting.RemoteEndPoint, out string text))
                return string.Format(NAME_FORMAT, text);
            else
                return NAME_DEFAULT;
        }
        #endregion

        #region Connect
        public ResultConnect Connect(bool blocking = true)
        {
            Socket? socket = null;
            try
            {
                socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                {
                    Blocking = blocking
                };
                if (Setting.LocalEndPoint != null)
                    socket.Bind(Setting.LocalEndPoint);
                //
                socket.Connect(Setting.RemoteEndPoint);
                //
                ClientControl client = new(socket, socketSetting);
                return ResultConnect.Success(client);
            }
            catch (SocketException ex)
            {
                SocketUtilities.Socket_Close(socket);
                return ResultConnect.Fail(ex.Message, ex.SocketErrorCode);
            }
            catch (Exception ex)
            {
                SocketUtilities.Socket_Close(socket);
                return ResultConnect.Fail(ex.Message, SocketError.SocketError);
            }
        }
        public async Task<ResultConnect> ConnectAsync(CancellationToken cancellationToken = default)
        {
            Socket? socket = null;
            try
            {
                socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                if (Setting.LocalEndPoint != null)
                    socket.Bind(Setting.LocalEndPoint);
                //
                await socket.ConnectAsync(Setting.RemoteEndPoint, cancellationToken);
                //
                ClientControl client = new(socket, socketSetting);
                return ResultConnect.Success(client);
            }
            catch (SocketException ex)
            {
                SocketUtilities.Socket_Close(socket);
                return ResultConnect.Fail(ex.Message, ex.SocketErrorCode);
            }
            catch (Exception ex)
            {
                SocketUtilities.Socket_Close(socket);
                return ResultConnect.Fail(ex.Message, SocketError.SocketError);
            }
        }
        public void Connect(SocketUtilities.OnConnect? callback, CancellationToken cancellationToken = default)
        {
            _ = ConnectAsync(cancellationToken).ContinueWith((task) =>
            {
                callback?.Invoke(task.Result);
            }, CancellationToken.None);
        }
        public void Connect<T>(SocketUtilities.OnConnect<T>? callback, T state, CancellationToken cancellationToken = default)
        {
            _ = ConnectAsync(cancellationToken).ContinueWith((task) =>
            {
                callback?.Invoke(task.Result, state);
            }, CancellationToken.None);
        }
        public void Connect<T1, T2>(SocketUtilities.OnConnect<T1, T2>? callback, T1 state1, T2 state2, CancellationToken cancellationToken = default)
        {
            _ = ConnectAsync(cancellationToken).ContinueWith((task) =>
            {
                callback?.Invoke(task.Result, state1, state2);
            }, CancellationToken.None);
        }
        public void Connect<T1, T2, T3>(SocketUtilities.OnConnect<T1, T2, T3>? callback, T1 state1, T2 state2, T3 state3, CancellationToken cancellationToken = default)
        {
            _ = ConnectAsync(cancellationToken).ContinueWith((task) =>
            {
                callback?.Invoke(task.Result, state1, state2, state3);
            }, CancellationToken.None);
        }
        public void Connect<T1, T2, T3, T4>(SocketUtilities.OnConnect<T1, T2, T3, T4>? callback, T1 state1, T2 state2, T3 state3, T4 state4, CancellationToken cancellationToken = default)
        {
            _ = ConnectAsync(cancellationToken).ContinueWith((task) =>
            {
                callback?.Invoke(task.Result, state1, state2, state3, state4);
            }, CancellationToken.None);
        }
        #endregion
    }
}
