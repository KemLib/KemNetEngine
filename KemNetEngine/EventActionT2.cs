namespace KemNetEngine
{
    public readonly struct EventAction<T1, T2>(Action<T1, T2>? action, T1 state1, T2 state2) : IEvent
    {
        #region Properties
        private readonly Action<T1, T2>? action = action;
        private readonly T1 state1 = state1;
        private readonly T2 state2 = state2;
        #endregion

        #region Method
        public void Run()
        {
            action?.Invoke(state1, state2);
        }
        #endregion
    }
}
