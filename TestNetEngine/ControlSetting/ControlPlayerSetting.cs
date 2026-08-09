using KemNetEngine.Server;
using KemNetEngine.Network;

namespace TestNetEngine.ControlSetting
{
    public partial class ControlPlayerSetting : UserControl
    {
        #region Properties
        private const string ERROR_MESSAGE_PER_FRAME_WRONG_FORMAT = "Message per Frame must be a number";
        #endregion

        #region Constructions
        public ControlPlayerSetting()
        {
            InitializeComponent();
        }
        #endregion

        #region Ui Events

        private void ControlPlayerSetting_Load(object sender, EventArgs e)
        {
            textbox_message_per_frame.Text = PlayerSetting.DEFAULT_RECEIVE_MESSAGE_PER_FRAME.ToString();
        }
        private void Button_Default_Click(object sender, EventArgs e)
        {
            textbox_message_per_frame.Text = PlayerSetting.DEFAULT_RECEIVE_MESSAGE_PER_FRAME.ToString();
        }
        #endregion

        #region Methods
        public bool TryGetSetting([NotNullWhen(true)] out PlayerSetting? setting, [NotNullWhen(false)] out string[]? errors)
        {
            List<string> listError = [];
            if (int.TryParse(textbox_message_per_frame.Text, out int message_per_frame))
            {
                if (!PlayerSetting.Check_ReceiveMessagePerFrame(message_per_frame, out string? tmpError))
                    listError.Add(tmpError);
            }
            else
                listError.Add(ERROR_MESSAGE_PER_FRAME_WRONG_FORMAT);
            if (!control_socket_setting.TryGetSocketSetting(out SocketSetting? socket, out string[]? arrayError))
                listError.AddRange(arrayError);
            //
            if (listError.Count == 0 && socket != null)
            {
                setting = new(message_per_frame, socket);
                errors = null;
                return true;
            }
            else
            {
                setting = null;
                errors = [.. listError];
                return false;
            }
        }
        #endregion
    }
}
