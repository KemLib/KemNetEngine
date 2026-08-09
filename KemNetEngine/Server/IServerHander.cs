namespace KemNetEngine.Server
{
    public interface IServerHander
    {
        #region Properties

        #endregion

        #region Method
        void Server_OnStart(ServerEngine server);
        void Server_OnUpdate(ServerEngine server);
        void Server_OnStop(ServerEngine server);
        IPlayerHander? Server_OnPlayerConnect(ServerEngine server, PlayerControl player);
        #endregion
    }
}
