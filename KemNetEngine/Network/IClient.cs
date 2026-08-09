using System.Net;

namespace KemNetEngine.Network
{
    public interface IClient : IDisposable
    {
        #region Properties
        readonly static IClient ClientNone = new ClientNone();

        delegate void OnDisconnectDelegate(ClientControl client);
        event OnDisconnectDelegate OnDisconnect;

        string Name
        {
            get;
        }
        EndPoint? LocalEndPoint
        {
            get;
        }
        EndPoint? RemoteEndPoint
        {
            get;
        }
        bool IsConnect
        {
            get;
        }
        bool IsDisposed
        {
            get;
        }
        SocketClose CloseFrom
        {
            get;
        }
        bool IsSending
        {
            get;
        }
        bool IsReceiving
        {
            get;
        }
        int DataAvailable
        {
            get;
        }
        #endregion

        #region Close
        void Close();
        #endregion

        #region Send
        ResultSend Send(ReadOnlyMemory<byte> message);
        Task<ResultSend> SendAsync(ReadOnlyMemory<byte> message, CancellationToken cancellationToken = default);
        void Send(ReadOnlyMemory<byte> message, SocketUtilities.OnSend? callback, CancellationToken cancellationToken = default);
        void Send<T>(ReadOnlyMemory<byte> message, SocketUtilities.OnSend<T>? callback, T state, CancellationToken cancellationToken = default);
        void Send<T1, T2>(ReadOnlyMemory<byte> message, SocketUtilities.OnSend<T1, T2>? callback, T1 state1, T2 state2, CancellationToken cancellationToken = default);
        void Send<T1, T2, T3>(ReadOnlyMemory<byte> message, SocketUtilities.OnSend<T1, T2, T3>? callback, T1 state1, T2 state2, T3 state3, CancellationToken cancellationToken = default);
        void Send<T1, T2, T3, T4>(ReadOnlyMemory<byte> message, SocketUtilities.OnSend<T1, T2, T3, T4>? callback, T1 state1, T2 state2, T3 state3, T4 state4, CancellationToken cancellationToken = default);
        #endregion

        #region Receive
        ResultReceive Receive();
        Task<ResultReceive> ReceiveAsync(CancellationToken cancellationToken = default);
        void Receive(SocketUtilities.OnReceive? callback, CancellationToken cancellationToken = default);
        void Receive<T>(SocketUtilities.OnReceive<T>? callback, T state, CancellationToken cancellationToken = default);
        void Receive<T1, T2>(SocketUtilities.OnReceive<T1, T2>? callback, T1 state1, T2 state2, CancellationToken cancellationToken = default);
        void Receive<T1, T2, T3>(SocketUtilities.OnReceive<T1, T2, T3>? callback, T1 state1, T2 state2, T3 state3, CancellationToken cancellationToken = default);
        void Receive<T1, T2, T3, T4>(SocketUtilities.OnReceive<T1, T2, T3, T4>? callback, T1 state1, T2 state2, T3 state3, T4 state4, CancellationToken cancellationToken = default);
        #endregion
    }
}
