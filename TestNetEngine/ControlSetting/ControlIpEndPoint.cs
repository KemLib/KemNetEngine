using KemNetEngine.Server;
using System.Net;

namespace TestNetEngine.ControlSetting
{
    public partial class ControlIpEndPoint : UserControl
    {
        #region Properties
        private const string ERROR_IP_WRONG_FORMAT = "Ip is not a valid IpAddress",
            ERROR_PORT_WRONG_FORMAT = "Port must be a number";
        private const string DEFAULT_IP = "127.0.0.1";
        private const int DEFAULT_PORT = 4321;
        #endregion

        #region Constrcutions
        public ControlIpEndPoint()
        {
            InitializeComponent();
        }
        #endregion

        #region Ui Events
        private void ControlIpEndPoint_Load(object sender, EventArgs e)
        {
            textbox_ip.Text = DEFAULT_IP;
            textbox_port.Text = DEFAULT_PORT.ToString();
        }
        private void Button_Default_Click(object sender, EventArgs e)
        {
            textbox_ip.Text = DEFAULT_IP;
            textbox_port.Text = DEFAULT_PORT.ToString();
        }
        #endregion

        #region Method
        public bool TryGetIpEndPoint([NotNullWhen(true)] out IPEndPoint? ipEndPoint, [NotNullWhen(false)] out string[]? errors)
        {
            List<string> listError = [];
            if (!IPAddress.TryParse(textbox_ip.Text, out IPAddress? ipAddress))
                listError.Add(ERROR_IP_WRONG_FORMAT);
            if (int.TryParse(textbox_port.Text, out int port))
            {
                if (!PlayerSetting.Check_ReceiveMessagePerFrame(port, out string? tmpError))
                    listError.Add(tmpError);
            }
            else
                listError.Add(ERROR_PORT_WRONG_FORMAT);
            //
            if (listError.Count == 0 && ipAddress != null)
            {
                ipEndPoint = new IPEndPoint(ipAddress, port);
                errors = null;
                return true;
            }
            else
            {
                ipEndPoint = null;
                errors = [.. listError];
                return false;
            }
        }
        #endregion
    }
}
