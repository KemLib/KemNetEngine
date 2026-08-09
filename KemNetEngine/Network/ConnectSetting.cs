using System.Net;

namespace KemNetEngine.Network
{
    public class ConnectSetting
    {
        #region Properties
        private const string ERROR_REMOTE_IP_IS_NULL = "Remote Ip is null",
            ERROR_LOCAL_IP_IS_NULL = "Local Ip is null",
            ERROR_REMOTE_IP_ENDPOINT_IS_NULL = "Remote IpEndPoint is null",
            ERROR_REMOTE_DNS_ENDPOINT_IS_NULL = "Remote DnsEndPoint is null",
            ERROR_REMOTE_ENDPOINT_IS_NULL = "Remote EndPoint is null",
            ERROR_LOCAL_IPENDPOINT_IS_NULL = "Local IpEndPoint is null";
        private const string NAME_DEFAULT = "ConnectSetting",
            NAME_FORMAT = "ConnectSetting({0})";

        public IPEndPoint? LocalEndPoint
        {
            get;
            init;
        }
        public EndPoint RemoteEndPoint
        {
            get;
            init;
        }
        #endregion

        #region Construction
        public ConnectSetting(byte[] remoteIp, int remotePort)
        {
            if (remoteIp == null)
                throw new ArgumentNullException(nameof(remoteIp), ERROR_REMOTE_IP_IS_NULL);
            //
            LocalEndPoint = null;
            IPAddress remoteIpAddress = new(remoteIp);
            RemoteEndPoint = new IPEndPoint(remoteIpAddress, remotePort);
        }
        public ConnectSetting(byte[] localIp, int localPort, byte[] remoteIp, int remotePort)
        {
            if (localIp == null)
                throw new ArgumentNullException(nameof(localIp), ERROR_LOCAL_IP_IS_NULL);
            if (remoteIp == null)
                throw new ArgumentNullException(nameof(remoteIp), ERROR_REMOTE_IP_IS_NULL);
            //
            IPAddress localIpAddress = new(localIp),
                remoteIpAddress = new(remoteIp);
            LocalEndPoint = new IPEndPoint(localIpAddress, localPort);
            RemoteEndPoint = new IPEndPoint(remoteIpAddress, remotePort);
        }
        public ConnectSetting(IPEndPoint remoteIpEndPoint)
        {
            if (remoteIpEndPoint == null)
                throw new ArgumentNullException(nameof(remoteIpEndPoint), ERROR_REMOTE_IP_ENDPOINT_IS_NULL);
            //
            LocalEndPoint = null;
            RemoteEndPoint = remoteIpEndPoint;
        }
        public ConnectSetting(IPEndPoint localIpEndPoint, IPEndPoint remoteIpEndPoint)
        {
            if (localIpEndPoint == null)
                throw new ArgumentNullException(nameof(localIpEndPoint), ERROR_LOCAL_IPENDPOINT_IS_NULL);
            if (remoteIpEndPoint == null)
                throw new ArgumentNullException(nameof(remoteIpEndPoint), ERROR_REMOTE_IP_ENDPOINT_IS_NULL);
            //
            LocalEndPoint = localIpEndPoint;
            RemoteEndPoint = remoteIpEndPoint;
        }
        public ConnectSetting(DnsEndPoint remoteDnsEndPoint)
        {
            if (remoteDnsEndPoint == null)
                throw new ArgumentNullException(nameof(remoteDnsEndPoint), ERROR_REMOTE_DNS_ENDPOINT_IS_NULL);
            //
            LocalEndPoint = null;
            RemoteEndPoint = remoteDnsEndPoint;
        }
        public ConnectSetting(IPEndPoint localIpEndPoint, DnsEndPoint remoteDnsEndPoint)
        {
            if (localIpEndPoint == null)
                throw new ArgumentNullException(nameof(localIpEndPoint), ERROR_LOCAL_IPENDPOINT_IS_NULL);
            if (remoteDnsEndPoint == null)
                throw new ArgumentNullException(nameof(remoteDnsEndPoint), ERROR_REMOTE_DNS_ENDPOINT_IS_NULL);
            //
            LocalEndPoint = localIpEndPoint;
            RemoteEndPoint = remoteDnsEndPoint;
        }
        public ConnectSetting(EndPoint remoteEndPoint)
        {
            if (remoteEndPoint == null)
                throw new ArgumentNullException(nameof(remoteEndPoint), ERROR_REMOTE_ENDPOINT_IS_NULL);
            //
            LocalEndPoint = null;
            RemoteEndPoint = remoteEndPoint;
        }
        public ConnectSetting(IPEndPoint localIpEndPoint, EndPoint remoteEndPoint)
        {
            if (localIpEndPoint == null)
                throw new ArgumentNullException(nameof(localIpEndPoint), ERROR_LOCAL_IPENDPOINT_IS_NULL);
            if (remoteEndPoint == null)
                throw new ArgumentNullException(nameof(remoteEndPoint), ERROR_REMOTE_ENDPOINT_IS_NULL);
            //
            LocalEndPoint = localIpEndPoint;
            RemoteEndPoint = remoteEndPoint;
        }
        #endregion

        #region Method
        public override string ToString()
        {
            if (SocketUtilities.TryGetText(LocalEndPoint, RemoteEndPoint, out string text))
                return string.Format(NAME_FORMAT, text);
            else
                return NAME_DEFAULT;

        }
        #endregion
    }
}
