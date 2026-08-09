namespace KemNetEngine
{
    public readonly struct EventAction(Action? action) : IEvent
    {
        #region Properties
        private readonly Action? action = action;
        #endregion

        #region Method
        public void Run()
        {
            action?.Invoke();
        }
        #endregion
    }
}
