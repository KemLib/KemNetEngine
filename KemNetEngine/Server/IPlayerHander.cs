using KemNetEngine.Network;

namespace KemNetEngine.Server
{
    public interface IPlayerHander
    {
        #region Properties

        #endregion

        #region Method
        void Player_OnStart(PlayerControl player);
        void Player_OnUpdate(PlayerControl player);
        void Player_OnStop(PlayerControl player);
        void Player_OnDisconnect(PlayerControl player, SocketClose close);
        void Player_OnReceive(PlayerControl player, byte[] message);
        #endregion
    }
}
