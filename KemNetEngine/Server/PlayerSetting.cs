using KemNetEngine.Network;

namespace KemNetEngine.Server
{
    public class PlayerSetting
    {
        #region Properties
        private const string ERROR_RECEIVE_MESSAGE_PER_FRAME_LESS_THAN_MIN = "Receive message per frame is less than {0}",
            ERROR_SOCKET_IS_NULL = "Socket is null";
        public const int RECEIVE_MESSAGE_PER_FRAME_MIN = 1,
            RECEIVE_MESSAGE_PER_FRAME_INFINITE = -1;
        public const int DEFAULT_RECEIVE_MESSAGE_PER_FRAME = 10;

        public int ReceiveMessagePerFrame
        {
            get;
            init;
        }
        public SocketSetting Socket
        {
            get;
            init;
        }
        #endregion

        #region Construction
        public PlayerSetting()
        {
            ReceiveMessagePerFrame = DEFAULT_RECEIVE_MESSAGE_PER_FRAME;
            Socket = new();
        }
        public PlayerSetting(int receiveRessagePerFrame, SocketSetting socket)
        {
            if (!Check_ReceiveMessagePerFrame(receiveRessagePerFrame, out string? error))
                throw new ArgumentOutOfRangeException(error);
            if (socket == null)
                throw new ArgumentNullException(nameof(socket), ERROR_SOCKET_IS_NULL);
            //
            ReceiveMessagePerFrame = receiveRessagePerFrame;
            Socket = socket;
        }
        #endregion

        #region Method
        public int GetRequestMessagePerFrame()
        {
            if (ReceiveMessagePerFrame > 0)
                return ReceiveMessagePerFrame;
            return int.MaxValue;
        }
        #endregion

        #region Check
        public static bool Check_ReceiveMessagePerFrame(int receiveRessagePerFrame, [NotNullWhen(false)] out string? error)
        {
            if (receiveRessagePerFrame == RECEIVE_MESSAGE_PER_FRAME_INFINITE)
            {
                error = null;
                return true;
            }
            //
            if (receiveRessagePerFrame < RECEIVE_MESSAGE_PER_FRAME_MIN)
            {
                error = string.Format(ERROR_RECEIVE_MESSAGE_PER_FRAME_LESS_THAN_MIN, RECEIVE_MESSAGE_PER_FRAME_MIN);
                return false;
            }
            //
            error = null;
            return true;
        }
        #endregion
    }
}
