using KemNetEngine.Server;
using System.Net;

namespace TestNetEngine.ControlSetting
{
    public partial class ControlDnsEndPoint : UserControl
    {
        #region Properties
        private const string ERROR_PORT_WRONG_FORMAT = "Port must be a number";
        private const string DEFAULT_HOST = "localhost";
        private const int DEFAULT_PORT = 4321;
        #endregion

        #region Constrcutions
        public ControlDnsEndPoint()
        {
            InitializeComponent();
        }
        #endregion

        #region Ui Events
        private void ControlIpEndPoint_Load(object sender, EventArgs e)
        {
            textbox_host.Text = DEFAULT_HOST;
            textbox_port.Text = DEFAULT_PORT.ToString();
        }
        private void Button_Default_Click(object sender, EventArgs e)
        {
            textbox_host.Text = DEFAULT_HOST;
            textbox_port.Text = DEFAULT_PORT.ToString();
        }
        #endregion

        #region Method
        public bool TryGetIpEndPoint([NotNullWhen(true)] out DnsEndPoint? dnsEndPoint, [NotNullWhen(false)] out string[]? errors)
        {
            List<string> listError = [];
            if (int.TryParse(textbox_port.Text, out int port))
            {
                if (!PlayerSetting.Check_ReceiveMessagePerFrame(port, out string? tmpError))
                    listError.Add(tmpError);
            }
            else
                listError.Add(ERROR_PORT_WRONG_FORMAT);
            //
            if (listError.Count == 0)
            {
                try
                {
                    dnsEndPoint = new DnsEndPoint(textbox_host.Text, port);
                    errors = null;
                    return true;
                }
                catch (Exception ex)
                {
                    listError.Add(ex.Message);
                }
            }
            //
            dnsEndPoint = null;
            errors = [.. listError];
            return false;
        }
        #endregion
    }
}
