using KemLibCore.DebugLog;
using KemNetEngine.Network;
using KemNetEngine.Server;

namespace TestNetEngine.Net.Server
{
    internal class PlayerHander(ILogHander logHander) : IPlayerHander
    {
        #region Properties
        private const string PLAYER_START = "Player start",
            PLAYER_DISCONNECT = "Player disconnect",
            PLAYER_STOP = "Player stop";

        private readonly ILogHander logHander = logHander;
        #endregion

        #region Construction
        #endregion

        #region Player
        public void Player_OnStart(PlayerControl player)
        {
            logHander.LogMessage(player.Name, PLAYER_START);
        }
        public void Player_OnStop(PlayerControl player)
        {
            logHander.LogMessage(player.Name, PLAYER_STOP);
            logHander.DisableLog();
        }
        public void Player_OnUpdate(PlayerControl player)
        {

        }
        public void Player_OnReceive(PlayerControl player, byte[] message)
        {
            int count = player.PlayerManager.Count;
            for (int i = 0; i < count; i++)
            {
                player.PlayerManager[i].Send(message);
            }
        }
        public void Player_OnDisconnect(PlayerControl player, SocketClose close)
        {
            logHander.LogMessage(player.Name, PLAYER_DISCONNECT, close.ToString());
        }
        #endregion
    }
}
