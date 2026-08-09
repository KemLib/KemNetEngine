using KemNetEngine.Network;

namespace KemNetEngine.Server
{
    internal readonly struct EventSend<T>(EventSend<T>.OnSend action, ResultSend result, SocketUtilities.OnSend<T> callback, T state) : IEvent
    {
        #region Properties
        public delegate void OnSend(ResultSend resul, SocketUtilities.OnSend<T> callback, T state);

        private readonly OnSend action = action;
        private readonly ResultSend result = result;
        private readonly SocketUtilities.OnSend<T> callback = callback;
        private readonly T state = state;
        #endregion

        #region Method
        public void Run()
        {
            action(result, callback, state);
        }
        #endregion
    }
}
