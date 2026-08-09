using KemNetEngine.Network;

namespace KemNetEngine.Server
{
    internal readonly struct EventSend<T1, T2>(EventSend<T1, T2>.OnSend action, ResultSend result, SocketUtilities.OnSend<T1, T2> callback, T1 state1, T2 state2) : IEvent
    {
        #region Properties
        public delegate void OnSend(ResultSend resul, SocketUtilities.OnSend<T1, T2> callback, T1 state1, T2 state2);

        private readonly OnSend action = action;
        private readonly ResultSend result = result;
        private readonly SocketUtilities.OnSend<T1, T2> callback = callback;
        private readonly T1 state1 = state1;
        private readonly T2 state2 = state2;
        #endregion

        #region Method
        public void Run()
        {
            action(result, callback, state1, state2);
        }
        #endregion
    }
}
