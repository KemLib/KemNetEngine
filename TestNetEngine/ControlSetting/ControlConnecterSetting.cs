using System.Net;
using KemNetEngine.Network;

namespace TestNetEngine.ControlSetting
{
    public partial class ControlConnecterSetting : UserControl
    {
        #region Properties

        #endregion

        #region Constructions
        public ControlConnecterSetting()
        {
            InitializeComponent();
        }
        #endregion

        #region UI Events
        private void ControlClientSetting_Load(object sender, EventArgs e)
        {
            checkbox_local_end_Point.Checked = false;
            control_local_ip_end_point.Enabled = false;
            combobox_remote_end_point.SelectedIndex = 0;
            control_remote_dns_end_point.Hide();
            control_remote_dns_end_point.Enabled = false;
            control_remote_ip_end_point.Show();
            control_remote_ip_end_point.Enabled = true;
        }
        private void Checkbox_Local_End_Point_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbox_local_end_Point.Checked)
                control_local_ip_end_point.Enabled = true;
            else
                control_local_ip_end_point.Enabled = false;
        }
        private void Combobox_Remote_End_Point_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combobox_remote_end_point.SelectedIndex == 0)
            {
                control_remote_dns_end_point.Hide();
                control_remote_dns_end_point.Enabled = false;
                control_remote_ip_end_point.Show();
                control_remote_ip_end_point.Enabled = true;
            }
            else
            {
                control_remote_dns_end_point.Show();
                control_remote_dns_end_point.Enabled = true;
                control_remote_ip_end_point.Hide();
                control_remote_ip_end_point.Enabled = false;
            }
        }
        #endregion

        #region Methods
        public bool TryGetConnectSetting([NotNullWhen(true)] out ConnectSetting? connectSetting, [NotNullWhen(false)] out string[]? errors)
        {
            List<string> listError = [];
            string[]? arrayError;
            IPEndPoint? localEndPoint;
            if (checkbox_local_end_Point.Checked)
            {
                if (!control_local_ip_end_point.TryGetIpEndPoint(out localEndPoint, out arrayError))
                {
                    localEndPoint = null;
                    listError.AddRange(arrayError);
                }
            }
            else
            {
                localEndPoint = null;
            }
            EndPoint? remoteEndPoint;
            if (combobox_remote_end_point.SelectedIndex == 0)
            {
                if (control_remote_ip_end_point.TryGetIpEndPoint(out IPEndPoint? remoteIpEndPoint, out arrayError))
                {
                    remoteEndPoint = remoteIpEndPoint;
                }
                else
                {
                    remoteEndPoint = null;
                    listError.AddRange(arrayError);
                }
            }
            else
            {
                if (control_remote_dns_end_point.TryGetIpEndPoint(out DnsEndPoint? remoteDnsEndPoint, out arrayError))
                {
                    remoteEndPoint = remoteDnsEndPoint;
                }
                else
                {
                    remoteEndPoint = null;
                    listError.AddRange(arrayError);
                }
            }
            //
            if (listError.Count == 0 && remoteEndPoint != null)
            {
                if (localEndPoint == null)
                    connectSetting = new(remoteEndPoint);
                else
                    connectSetting = new(localEndPoint, remoteEndPoint);
                errors = null;
                return true;
            }
            else
            {
                connectSetting = null;
                errors = [.. listError];
                return false;
            }
        }
        #endregion
    }
}
