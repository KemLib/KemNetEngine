using KemNetEngine.Network;

namespace KemNetEngine.Server
{
    internal class PlayerHanderNone : IPlayerHander
    {
        #region Properties
        public readonly static PlayerHanderNone Instance = new();
        #endregion

        #region Constructors

        #endregion

        #region Methods
        public void Player_OnStart(PlayerControl player)
        {

        }
        public void Player_OnUpdate(PlayerControl player)
        {

        }
        public void Player_OnStop(PlayerControl player)
        {

        }
        public void Player_OnDisconnect(PlayerControl player, SocketClose close)
        {

        }
        public void Player_OnReceive(PlayerControl player, byte[] data)
        {

        }
        #endregion
    }
}
