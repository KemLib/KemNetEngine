using KemLibCore.DebugLog;
using KemNetEngine.Server;

namespace TestNetEngine.Net.Server
{
    internal class ServerHander(ILogHander logHander) : IServerHander
    {
        #region Properties
        private readonly ILogHander logHander = logHander;
        #endregion

        #region Construction

        #endregion

        #region Server
        public void Server_OnStart(ServerEngine server)
        {

        }
        public void Server_OnStop(ServerEngine server)
        {
            logHander.DisableLog();
        }
        public void Server_OnUpdate(ServerEngine server)
        {

        }
        public IPlayerHander? Server_OnPlayerConnect(ServerEngine server, PlayerControl player)
        {
            ILogHander newLogHander = logHander.CreateHander();
            return new PlayerHander(newLogHander);
        }
        #endregion
    }
}
