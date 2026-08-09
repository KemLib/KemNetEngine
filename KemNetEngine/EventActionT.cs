namespace KemNetEngine
{
    public readonly struct EventAction<T>(Action<T>? action, T state) : IEvent
    {
        #region Properties
        private readonly Action<T>? action = action;
        private readonly T state = state;
        #endregion

        #region Method
        public void Run()
        {
            action?.Invoke(state);
        }
        #endregion
    }
}
