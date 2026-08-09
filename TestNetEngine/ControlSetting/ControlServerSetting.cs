using KemNetEngine.Server;
using KemNetEngine.Network;

namespace TestNetEngine.ControlSetting
{
    public partial class ControlServerSetting : UserControl
    {
        #region Properties
        private const string ERROR_LIMIT_PLAYER_WRONG_FORMAT = "Limit Player must be a number",
            ERROR_CLIENT_PER_FRAME_WRONG_FORMAT = "Limit Player must be a number";
        #endregion

        #region Constructions
        public ControlServerSetting()
        {
            InitializeComponent();
        }
        #endregion

        #region Ui Events
        private void ControlServerSetting_Load(object sender, EventArgs e)
        {
            textbox_limit_player.Text = ServerSetting.DEFAULT_LIMIT_PLAYER.ToString();
            textbox_client_per_frame.Text = ServerSetting.DEFAULT_ACCEPT_CLIENT_PER_FRAME.ToString();
        }
        private void Button_Default_Click(object sender, EventArgs e)
        {
            textbox_limit_player.Text = ServerSetting.DEFAULT_LIMIT_PLAYER.ToString();
            textbox_client_per_frame.Text = ServerSetting.DEFAULT_ACCEPT_CLIENT_PER_FRAME.ToString();
        }
        #endregion

        #region Methods
        public bool TryGetServerSetting([NotNullWhen(true)] out ServerSetting? server, [NotNullWhen(false)] out string[]? errors)
        {
            List<string> listError = [];
            if (!control_listen_setting.TryGetListenSetting(out ListenSetting? listenSetting, out string[]? arrayError))
                listError.AddRange(arrayError);
            if (!control_player_setting.TryGetSetting(out PlayerSetting? player, out arrayError))
                listError.AddRange(arrayError);
            if (int.TryParse(textbox_limit_player.Text, out int limitPlayer))
            {
                if (!ServerSetting.Check_LimitPlayer(limitPlayer, out string? tmpError))
                    listError.Add(tmpError);
            }
            else
                listError.Add(ERROR_LIMIT_PLAYER_WRONG_FORMAT);
            if (int.TryParse(textbox_client_per_frame.Text, out int clientPerFrame))
            {
                if (!ServerSetting.Check_AcceptClientPerFrame(clientPerFrame, out string? tmpError))
                    listError.Add(tmpError);
            }
            else
                listError.Add(ERROR_CLIENT_PER_FRAME_WRONG_FORMAT);
            //
            if (listError.Count == 0 && listenSetting != null && player != null)
            {
                server = new ServerSetting(listenSetting, player, limitPlayer, clientPerFrame);
                errors = null;
                return true;
            }
            else
            {
                server = null;
                errors = [.. listError];
                return false;
            }
        }
        #endregion
    }
}
