using KemNetEngine.Network;

namespace KemNetEngine.Server
{
    internal readonly struct EventSend<T1, T2, T3, T4>(EventSend<T1, T2, T3, T4>.OnSend action, ResultSend result, SocketUtilities.OnSend<T1, T2, T3, T4> callback, T1 state1, T2 state2, T3 state3, T4 state4) : IEvent
    {
        #region Properties
        public delegate void OnSend(ResultSend resul, SocketUtilities.OnSend<T1, T2, T3, T4> callback, T1 state1, T2 state2, T3 state3, T4 state4);

        private readonly OnSend action = action;
        private readonly ResultSend result = result;
        private readonly SocketUtilities.OnSend<T1, T2, T3, T4> callback = callback;
        private readonly T1 state1 = state1;
        private readonly T2 state2 = state2;
        private readonly T3 state3 = state3;
        private readonly T4 state4 = state4;
        #endregion

        #region Method
        public void Run()
        {
            action(result, callback, state1, state2, state3, state4);
        }
        #endregion
    }
}
