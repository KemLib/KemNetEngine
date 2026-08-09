using KemNetEngine.Network;

namespace KemNetEngine.Server
{
    internal readonly struct EventSend(EventSend.OnSend action, ResultSend result, SocketUtilities.OnSend callback) : IEvent
    {
        #region Properties
        public delegate void OnSend(ResultSend resul, SocketUtilities.OnSend callback);

        private readonly OnSend action = action;
        private readonly ResultSend result = result;
        private readonly SocketUtilities.OnSend callback = callback;
        #endregion

        #region Method
        public void Run()
        {
            action(result, callback);
        }
        #endregion
    }
}
