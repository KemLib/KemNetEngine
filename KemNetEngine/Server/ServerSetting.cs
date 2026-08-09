using KemNetEngine.Network;

namespace KemNetEngine.Server
{
    public class ServerSetting
    {
        #region Properties
        private const string ERROR_LISTEN_IS_NULL = "Listen is null",
            ERROR_PLAYER_IS_NULL = "Player is null",
            ERROR_LIMIT_PLAYER_LESS_THAN_MIN = "Limit player is less than {0}",
            ERROR_ACCEPT_CLIENT_PER_FRAME_LESS_THAN_MIN = "Accept client per frame is less than {0}";
        public const int LIMIT_PLAYER_MIN = 1,
            LIMIT_PLAYER_INFINITE = -1,
            ACCEPT_CLIENT_PER_FRAME_MIN = 1,
            ACCEPT_CLIENT_PER_FRAME_INFINITE = -1;
        private const int PLAYERS_CAPACITY_MAX = 1000;
        public const int DEFAULT_LIMIT_PLAYER = 100,
            DEFAULT_ACCEPT_CLIENT_PER_FRAME = 10;

        public readonly ListenSetting Listen;
        public readonly PlayerSetting Player;
        public int LimitPlayer
        {
            get;
            init;
        }
        public int AcceptClientPerFrame
        {
            get;
            init;
        }
        #endregion

        #region Constructions
        public ServerSetting(ListenSetting listen, PlayerSetting player)
        {
            if (listen == null)
                throw new ArgumentNullException(nameof(listen), ERROR_LISTEN_IS_NULL);
            if (player == null)
                throw new ArgumentNullException(nameof(player), ERROR_PLAYER_IS_NULL);
            //
            Listen = listen;
            Player = player;
            LimitPlayer = DEFAULT_LIMIT_PLAYER;
            AcceptClientPerFrame = DEFAULT_ACCEPT_CLIENT_PER_FRAME;
        }
        public ServerSetting(ListenSetting listen, PlayerSetting player, int limitPlayer, int acceptClientPerFrame)
        {
            if (listen == null)
                throw new ArgumentNullException(nameof(listen), ERROR_LISTEN_IS_NULL);
            if (player == null)
                throw new ArgumentNullException(nameof(player), ERROR_PLAYER_IS_NULL);
            if (!Check_LimitPlayer(limitPlayer, out string? error))
                throw new ArgumentOutOfRangeException(nameof(limitPlayer), error);
            if (!Check_AcceptClientPerFrame(acceptClientPerFrame, out error))
                throw new ArgumentOutOfRangeException(nameof(acceptClientPerFrame), error);
            //
            Listen = listen;
            Player = player;
            LimitPlayer = limitPlayer;
            AcceptClientPerFrame = acceptClientPerFrame;
        }
        #endregion

        #region Method
        public int GetPlayersCapacity()
        {
            if (LimitPlayer == LIMIT_PLAYER_INFINITE)
                return PLAYERS_CAPACITY_MAX;
            //
            return Math.Min(LimitPlayer, PLAYERS_CAPACITY_MAX);
        }
        public int GetRequestClientPerFrame(int currentPlayer)
        {
            if (LimitPlayer > 0)
            {
                if (currentPlayer < 0)
                    currentPlayer = 0;
                int capacity = LimitPlayer - currentPlayer;
                if (capacity < 0)
                    return 0;
                //
                return Math.Min(AcceptClientPerFrame, capacity);
            }
            else
            {
                return Math.Min(AcceptClientPerFrame, int.MaxValue);
            }
        }
        #endregion

        #region Check
        public static bool Check_LimitPlayer(int limitPlayer, [NotNullWhen(false)] out string? error)
        {
            if (limitPlayer == LIMIT_PLAYER_INFINITE)
            {
                error = null;
                return true;
            }
            //
            if (limitPlayer < LIMIT_PLAYER_MIN)
            {
                error = string.Format(ERROR_LIMIT_PLAYER_LESS_THAN_MIN, LIMIT_PLAYER_MIN);
                return false;
            }
            //
            error = null;
            return true;
        }
        public static bool Check_AcceptClientPerFrame(int acceptClientPerFrame, [NotNullWhen(false)] out string? error)
        {
            if (acceptClientPerFrame == ACCEPT_CLIENT_PER_FRAME_INFINITE)
            {
                error = null;
                return true;
            }
            //
            if (acceptClientPerFrame < ACCEPT_CLIENT_PER_FRAME_MIN)
            {
                error = string.Format(ERROR_ACCEPT_CLIENT_PER_FRAME_LESS_THAN_MIN, ACCEPT_CLIENT_PER_FRAME_MIN);
                return false;
            }
            //
            error = null;
            return true;
        }
        #endregion
    }
}
