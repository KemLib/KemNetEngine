using System.Net;
using System.Net.Sockets;

namespace KemNetEngine.Network
{
    public class ClientNone : IClient
    {
        #region Properties
        protected const string ERROR_SOCKET_IS_NOT_CONNECTED = "the Socket is not connected";
        private const string NAME_DEFAULT = "Client";

        public event IClient.OnDisconnectDelegate OnDisconnect
        {
            add
            {

            }
            remove
            {

            }
        }

        public string Name => NAME_DEFAULT;
        public EndPoint? LocalEndPoint => null;
        public EndPoint? RemoteEndPoint => null;
        public bool IsConnect => false;
        public bool IsDisposed => false;
        public SocketClose CloseFrom => SocketClose.Local;
        public bool IsSending => false;
        public bool IsReceiving => false;
        public int DataAvailable => -1;
        #endregion

        #region Constructions
        public ClientNone()
        {

        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return NAME_DEFAULT;
        }
        #endregion

        #region Close
        public void Close()
        {

        }
        #endregion

        #region Send
        public ResultSend Send(ReadOnlyMemory<byte> message)
        {
            return ResultSend.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected);
        }
        public async Task<ResultSend> SendAsync(ReadOnlyMemory<byte> message, CancellationToken cancellationToken = default)
        {
            return ResultSend.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected);
        }
        public void Send(ReadOnlyMemory<byte> message, SocketUtilities.OnSend? callback, CancellationToken cancellationToken = default)
        {
            callback?.Invoke(ResultSend.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected));
        }
        public void Send<T>(ReadOnlyMemory<byte> message, SocketUtilities.OnSend<T>? callback, T state, CancellationToken cancellationToken = default)
        {
            callback?.Invoke(ResultSend.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected), state);
        }
        public void Send<T1, T2>(ReadOnlyMemory<byte> message, SocketUtilities.OnSend<T1, T2>? callback, T1 state1, T2 state2, CancellationToken cancellationToken = default)
        {
            callback?.Invoke(ResultSend.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected), state1, state2);
        }
        public void Send<T1, T2, T3>(ReadOnlyMemory<byte> message, SocketUtilities.OnSend<T1, T2, T3>? callback, T1 state1, T2 state2, T3 state3, CancellationToken cancellationToken = default)
        {
            callback?.Invoke(ResultSend.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected), state1, state2, state3);
        }
        public void Send<T1, T2, T3, T4>(ReadOnlyMemory<byte> message, SocketUtilities.OnSend<T1, T2, T3, T4>? callback, T1 state1, T2 state2, T3 state3, T4 state4, CancellationToken cancellationToken = default)
        {
            callback?.Invoke(ResultSend.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected), state1, state2, state3, state4);
        }
        #endregion

        #region Receive
        public ResultReceive Receive()
        {
            return ResultReceive.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected);
        }
        public async Task<ResultReceive> ReceiveAsync(CancellationToken cancellationToken = default)
        {
            return ResultReceive.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected);
        }
        public void Receive(SocketUtilities.OnReceive? callback, CancellationToken cancellationToken = default)
        {
            callback?.Invoke(ResultReceive.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected));
        }
        public void Receive<T>(SocketUtilities.OnReceive<T>? callback, T state, CancellationToken cancellationToken = default)
        {
            callback?.Invoke(ResultReceive.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected), state);
        }
        public void Receive<T1, T2>(SocketUtilities.OnReceive<T1, T2>? callback, T1 state1, T2 state2, CancellationToken cancellationToken = default)
        {
            callback?.Invoke(ResultReceive.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected), state1, state2);
        }
        public void Receive<T1, T2, T3>(SocketUtilities.OnReceive<T1, T2, T3>? callback, T1 state1, T2 state2, T3 state3, CancellationToken cancellationToken = default)
        {
            callback?.Invoke(ResultReceive.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected), state1, state2, state3);
        }
        public void Receive<T1, T2, T3, T4>(SocketUtilities.OnReceive<T1, T2, T3, T4>? callback, T1 state1, T2 state2, T3 state3, T4 state4, CancellationToken cancellationToken = default)
        {
            callback?.Invoke(ResultReceive.Fail(ERROR_SOCKET_IS_NOT_CONNECTED, SocketError.NotConnected), state1, state2, state3, state4);
        }
        #endregion
    }
}
