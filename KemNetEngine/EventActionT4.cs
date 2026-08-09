namespace KemNetEngine
{
    public readonly struct EventAction<T1, T2, T3, T4>(Action<T1, T2, T3, T4>? action, T1 state1, T2 state2, T3 state3, T4 state4) : IEvent
    {
        #region Properties
        private readonly Action<T1, T2, T3, T4>? action = action;
        private readonly T1 state1 = state1;
        private readonly T2 state2 = state2;
        private readonly T3 state3 = state3;
        private readonly T4 state4 = state4;
        #endregion

        #region Method
        public void Run()
        {
            action?.Invoke(state1, state2, state3, state4);
        }
        #endregion
    }
}
