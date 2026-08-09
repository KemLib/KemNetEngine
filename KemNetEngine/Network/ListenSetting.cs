using System.Net;

namespace KemNetEngine.Network
{
    public class ListenSetting
    {
        #region Properties
        private const string ERROR_IP_IS_NULL = "Ip is null",
            ERROR_IP_CORRECT_FORMAT = "IP address is not in the correct format",
            ERROR_ENDPOINT_IS_NULL = "endPoint is null",
            ERROR_BACKLOG_CAPACITY_LESS_THAN_MIN = "Backlog capacity is less than {0}",
            ERROR_BACKLOG_CAPACITY_GREATER_THAN_MAX = "Backlog capacity is greater than {0}";
        private const string NAME_FOTMAT = "ListenSetting({0})";
        private const int BACKLOG_CAPACITY_MIN = 1,
            BACKLOG_CAPACITY_MAX = 1000;
        public const int DEFAULT_BACKLOG_CAPACITY = 100;

        public IPEndPoint EndPoint
        {
            get;
            init;
        }
        public int BacklogCapacity
        {
            get;
            init;
        }
        #endregion

        #region Construction
        public ListenSetting(byte[] ip, int port)
        {
            if (ip == null)
                throw new ArgumentNullException(nameof(ip), ERROR_IP_IS_NULL);
            //
            IPAddress address = new(ip);
            EndPoint = new IPEndPoint(address, port);
            BacklogCapacity = DEFAULT_BACKLOG_CAPACITY;
        }
        public ListenSetting(string ip, int port)
        {
            if (ip == null)
                throw new ArgumentNullException(nameof(ip), ERROR_IP_IS_NULL);
            if (!IPAddress.TryParse(ip, out IPAddress? address))
                throw new FormatException(ERROR_IP_CORRECT_FORMAT);
            //
            EndPoint = new IPEndPoint(address, port);
            BacklogCapacity = DEFAULT_BACKLOG_CAPACITY;
        }
        public ListenSetting(IPEndPoint endPoint, int backlogCapacity)
        {
            if (endPoint == null)
                throw new ArgumentNullException(nameof(endPoint), ERROR_ENDPOINT_IS_NULL);
            if (!Check_BacklogCapacity(backlogCapacity, out string? error))
                throw new ArgumentOutOfRangeException(nameof(backlogCapacity), error);
            //
            EndPoint = endPoint;
            BacklogCapacity = backlogCapacity;
        }
        #endregion

        #region Method
        public override string ToString()
        {
            return string.Format(NAME_FOTMAT, EndPoint.ToString());
        }
        #endregion

        #region Check
        public static bool Check_BacklogCapacity(int backlogCapacity, [NotNullWhen(false)] out string? error)
        {
            if (backlogCapacity < BACKLOG_CAPACITY_MIN)
            {
                error = string.Format(ERROR_BACKLOG_CAPACITY_LESS_THAN_MIN, BACKLOG_CAPACITY_MIN);
                return false;
            }
            //
            if (backlogCapacity > BACKLOG_CAPACITY_MAX)
            {
                error = string.Format(ERROR_BACKLOG_CAPACITY_GREATER_THAN_MAX, BACKLOG_CAPACITY_MAX);
                return false;
            }
            //
            error = null;
            return true;
        }
        #endregion
    }
}
