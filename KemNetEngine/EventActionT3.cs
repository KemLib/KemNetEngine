namespace KemNetEngine
{
    public readonly struct EventAction<T1, T2, T3>(Action<T1, T2, T3>? action, T1 state1, T2 state2, T3 state3) : IEvent
    {
        #region Properties
        private readonly Action<T1, T2, T3>? action = action;
        private readonly T1 state1 = state1;
        private readonly T2 state2 = state2;
        private readonly T3 state3 = state3;
        #endregion

        #region Method
        public void Run()
        {
            action?.Invoke(state1, state2, state3);
        }
        #endregion
    }
}
