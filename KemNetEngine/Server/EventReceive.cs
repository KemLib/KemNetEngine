namespace KemNetEngine.Server
{
    internal readonly struct EventReceive(EventReceive.OnReceive action, byte[] data) : IEvent
    {
        #region Properties
        public delegate void OnReceive(byte[] data);

        private readonly OnReceive action = action;
        private readonly byte[] data = data;
        #endregion

        #region Method
        public void Run()
        {
            action(data);
        }
        #endregion
    }
}
