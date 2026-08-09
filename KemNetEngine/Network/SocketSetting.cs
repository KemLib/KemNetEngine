using System.Net.Sockets;

namespace KemNetEngine.Network
{
    public class SocketSetting
    {
        #region Properties
        private const string ERROR_SEND_BUFFER_SIZE_LESS_THAN_MIN = "Send BufferSize is less than {0}",
            ERROR_SEND_BUFFER_SIZE_GREATER_THAN_MAX = "Send BufferSize is greater than {0}",
            ERROR_SEND_TIMEOUT_LESS_THAN_MIN = "Send Timeout is less than {0}",
            ERROR_SEND_TIMEOUT_GREATER_THAN_MAX = "Send Timeout is greater than {0}",
            ERROR_RECEIVE_BUFFER_SIZE_LESS_THAN_MIN = "Receive BufferSize is less than {0}",
            ERROR_RECEIVE_BUFFER_SIZE_GREATER_THAN_MAX = "Receive BufferSize is greater than {0}",
            ERROR_RECEIVE_TIMEOUT_LESS_THAN_MIN = "Receive Timeout is less than {0}",
            ERROR_RECEIVE_TIMEOUT_GREATER_THAN_MAX = "Receive Timeout is greater than {0}";
        public const int SEND_BUFFER_SIZE_MIN = 64,
            SEND_BUFFER_SIZE_MAX = 1024 * 1024 * 8,
            SEND_TIMEOUT_MIN = 512,
            SEND_TIMEOUT_MAX = 1000 * 60,
            RECEIVE_BUFFER_SIZE_MIN = 64,
            RECEIVE_BUFFER_SIZE_MAX = 1024 * 1024 * 8,
            RECEIVE_TIMEOUT_MIN = 512,
            RECEIVE_TIMEOUT_MAX = 1000 * 60;
        public const int DEFAULT_SEND_BUFFER_SIZE = 1024 * 64,
            DEFAULT_SEND_TIMEOUT = 1000 * 5,
            DEFAULT_RECEIVE_BUFFER_SIZE = 1024 * 64,
            DEFAULT_RECEIVE_TIMEOUT = 1000 * 5;
        public const bool DEFAULT_NO_DELAY = false;

        public int SendBufferSize
        {
            get;
            init;
        }
        public int SendTimeout
        {
            get;
            init;
        }
        public int ReceiveBufferSize
        {
            get;
            init;
        }
        public int ReceiveTimeout
        {
            get;
            init;
        }
        public bool NoDelay
        {
            get;
            init;
        }
        #endregion

        #region Construction
        public SocketSetting()
        {
            SendBufferSize = DEFAULT_SEND_BUFFER_SIZE;
            SendTimeout = DEFAULT_SEND_TIMEOUT;
            ReceiveBufferSize = DEFAULT_RECEIVE_BUFFER_SIZE;
            ReceiveTimeout = DEFAULT_RECEIVE_TIMEOUT;
            NoDelay = DEFAULT_NO_DELAY;
        }
        public SocketSetting(int sendBufferSize, int sendTimeout, int receiveBufferSize, int receiveTimeout, bool noDelay)
        {
            if (!Check_SendBufferSize(sendBufferSize, out string? error))
                throw new ArgumentOutOfRangeException(nameof(sendBufferSize), error);
            if (!Check_SendTimeout(sendTimeout, out error))
                throw new ArgumentOutOfRangeException(nameof(sendTimeout), error);
            if (!Check_ReceiveBufferSize(receiveBufferSize, out error))
                throw new ArgumentOutOfRangeException(nameof(receiveBufferSize), error);
            if (!Check_ReceiveTimeout(receiveTimeout, out error))
                throw new ArgumentOutOfRangeException(nameof(receiveTimeout), error);
            //
            SendBufferSize = sendBufferSize;
            SendTimeout = sendTimeout;
            ReceiveBufferSize = receiveBufferSize;
            ReceiveTimeout = receiveTimeout;
            NoDelay = noDelay;
        }
        #endregion

        #region Method
        public void ApplyTo(Socket socket)
        {
            socket.SendBufferSize = SendBufferSize;
            socket.SendTimeout = SendTimeout;
            socket.ReceiveBufferSize = ReceiveBufferSize;
            socket.ReceiveTimeout = ReceiveTimeout;
            socket.NoDelay = NoDelay;
        }
        #endregion

        #region Check
        public static bool Check_SendBufferSize(int sendBufferSize, [NotNullWhen(false)] out string? error)
        {
            if (sendBufferSize < SEND_BUFFER_SIZE_MIN)
            {
                error = string.Format(ERROR_SEND_BUFFER_SIZE_LESS_THAN_MIN, SEND_BUFFER_SIZE_MIN);
                return false;
            }
            //
            if (sendBufferSize > SEND_BUFFER_SIZE_MAX)
            {
                error = string.Format(ERROR_SEND_BUFFER_SIZE_GREATER_THAN_MAX, SEND_BUFFER_SIZE_MAX);
                return false;
            }
            //
            error = null;
            return true;
        }
        public static bool Check_SendTimeout(int sendTimeout, [NotNullWhen(false)] out string? error)
        {
            if (sendTimeout < SEND_TIMEOUT_MIN)
            {
                error = string.Format(ERROR_SEND_TIMEOUT_LESS_THAN_MIN, SEND_TIMEOUT_MIN);
                return false;
            }
            //
            if (sendTimeout > SEND_TIMEOUT_MAX)
            {
                error = string.Format(ERROR_SEND_TIMEOUT_GREATER_THAN_MAX, SEND_TIMEOUT_MAX);
                return false;
            }
            //
            error = null;
            return true;
        }
        public static bool Check_ReceiveBufferSize(int receiveBufferSize, [NotNullWhen(false)] out string? error)
        {
            if (receiveBufferSize < RECEIVE_BUFFER_SIZE_MIN)
            {
                error = string.Format(ERROR_RECEIVE_BUFFER_SIZE_LESS_THAN_MIN, RECEIVE_BUFFER_SIZE_MIN);
                return false;
            }
            //
            if (receiveBufferSize > RECEIVE_BUFFER_SIZE_MAX)
            {
                error = string.Format(ERROR_RECEIVE_BUFFER_SIZE_GREATER_THAN_MAX, RECEIVE_BUFFER_SIZE_MAX);
                return false;
            }
            //
            error = null;
            return true;
        }
        public static bool Check_ReceiveTimeout(int receiveTimeout, [NotNullWhen(false)] out string? error)
        {
            if (receiveTimeout < RECEIVE_TIMEOUT_MIN)
            {
                error = string.Format(ERROR_RECEIVE_TIMEOUT_LESS_THAN_MIN, RECEIVE_TIMEOUT_MIN);
                return false;
            }
            //
            if (receiveTimeout > RECEIVE_TIMEOUT_MAX)
            {
                error = string.Format(ERROR_RECEIVE_TIMEOUT_GREATER_THAN_MAX, RECEIVE_TIMEOUT_MAX);
                return false;
            }
            //
            error = null;
            return true;
        }
        #endregion
    }
}
