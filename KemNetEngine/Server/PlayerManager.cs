using KemLibCore.Concurrent.Queue;
using KemLibCore.DebugLog;
using System.Net.Sockets;

namespace KemNetEngine.Server
{
    public class PlayerManager
    {
        #region Properties
        public readonly ServerEngine ServerEngine;
        private readonly List<PlayerControl> players;
        private readonly List<int> removePlayers;

        public int Count => players.Count;
        public PlayerControl this[int index] => players[index];
        #endregion

        #region Construction
        internal PlayerManager(ServerEngine serverEngine)
        {
            ServerEngine = serverEngine;
            players = new(serverEngine.Setting.GetPlayersCapacity());
            removePlayers = [];
        }
        #endregion

        #region Player
        internal void Player_Update()
        {
            int index = 0;
            while (index < Count)
            {
                PlayerControl player = players[index];
                if (player.IsConnect)
                    player.Update();
                else if (!player.IsWorking)
                    removePlayers.Add(index);
                //
                index++;
            }
        }
        internal void Player_Remove()
        {
            for (int i = removePlayers.Count - 1; i >= 0; i--)
            {
                int index = removePlayers[i];
                players[index].Stop();
                players.RemoveAt(index);
            }
            removePlayers.Clear();
        }
        internal void Player_Stoping()
        {
            foreach (var player in players)
                player.Close();
        }
        internal void Player_StopUpdate()
        {
            int index = 0;
            while (index < Count)
            {
                if (!players[index].IsWorking)
                    removePlayers.Add(index);
                //
                index++;
            }
        }
        internal void Player_Accept(Socket[] arrayClient, ILogHander logHander)
        {
            int indexNewPlayer = players.Count;
            foreach (var client in arrayClient)
            {
                IQueueIn<IEvent> queueReceive = ServerEngine.Event_CreateQueue(),
                    queueSend = ServerEngine.Event_CreateQueue();
                ILogHander newLogHander = logHander.CreateHander();
                PlayerControl player = new(this, client, queueSend, queueReceive, newLogHander);
                players.Add(player);
            }
            //
            for (int i = indexNewPlayer; i < players.Count; i++)
            {
                players[i].Start();
            }
        }
        internal async Task Player_AcceptAsync(Socket[] arrayClient, ILogHander logHander)
        {
            int indexNewPlayer = players.Count;
            foreach (var client in arrayClient)
            {
                IQueueIn<IEvent> queueReceive = await ServerEngine.Event_CreateQueueAsync(),
                    queueSend = await ServerEngine.Event_CreateQueueAsync();
                ILogHander newLogHander = await logHander.CreateHanderAsync();
                PlayerControl player = new(this, client, queueReceive, queueSend, newLogHander);
                players.Add(player);
            }
            //
            for (int i = indexNewPlayer; i < players.Count; i++)
            {
                players[i].Start();
            }
        }
        #endregion
    }
}
