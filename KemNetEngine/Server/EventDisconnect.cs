using KemNetEngine.Network;

namespace KemNetEngine.Server
{
    internal readonly struct EventDisconnect(EventDisconnect.OnDisconnect action, SocketClose close) : IEvent
    {
        #region Properties
        public delegate void OnDisconnect(SocketClose close);

        private readonly OnDisconnect action = action;
        private readonly SocketClose close = close;
        #endregion

        #region Method
        public void Run()
        {
            action(close);
        }
        #endregion
    }
}
