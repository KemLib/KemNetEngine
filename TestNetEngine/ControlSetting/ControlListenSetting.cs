using System.Net;
using KemNetEngine.Server;
using KemNetEngine.Network;

namespace TestNetEngine.ControlSetting
{
    public partial class ControlListenSetting : UserControl
    {
        #region Properties
        private const string ERROR_BACKLOG_WRONG_FORMAT = "Backlog must be a number";
        #endregion

        #region Constructions
        public ControlListenSetting()
        {
            InitializeComponent();
        }
        #endregion

        #region Ui Events
        private void ControlListenSetting_Load(object sender, EventArgs e)
        {
            textbox_backlog.Text = ListenSetting.DEFAULT_BACKLOG_CAPACITY.ToString();
        }
        private void Button_Default_Click(object sender, EventArgs e)
        {
            textbox_backlog.Text = ListenSetting.DEFAULT_BACKLOG_CAPACITY.ToString();
        }
        #endregion

        #region Methods
        public bool TryGetListenSetting([NotNullWhen(true)] out ListenSetting? listenSetting, [NotNullWhen(false)] out string[]? errors)
        {
            List<string> listError = [];
            if (!control_ip_end_point.TryGetIpEndPoint(out IPEndPoint? ipEndPoint, out string[]? arrayError))
                listError.AddRange(arrayError);
            if (int.TryParse(textbox_backlog.Text, out int backlog))
            {
                if (!PlayerSetting.Check_ReceiveMessagePerFrame(backlog, out string? tmpError))
                    listError.Add(tmpError);
            }
            else
                listError.Add(ERROR_BACKLOG_WRONG_FORMAT);
            //
            if (listError.Count == 0 && ipEndPoint != null)
            {
                listenSetting = new(ipEndPoint, backlog);
                errors = null;
                return true;
            }
            else
            {
                listenSetting = null;
                errors = [.. listError];
                return false;
            }
        }
        #endregion
    }
}
