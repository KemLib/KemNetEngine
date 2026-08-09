using KemNetEngine.Network;

namespace TestNetEngine.ControlSetting
{
    public partial class ControlSocketSetting : UserControl
    {
        #region Properties
        private const string ERROR_SEND_BUFFER_SIZE_WRONG_FORMAT = "Send BufferSize must be a number",
            ERROR_SEND_TIMEOUT_WRONG_FORMAT = "Send Timeout must be a number",
            ERROR_RECEIVE_BUFFER_SIZE_WRONG_FORMAT = "Receive BufferSize must be a number",
            ERROR_RECEIVE_TIMEOUT_WRONG_FORMAT = "Receive Timeout must be a number";
        #endregion

        #region Constructions
        public ControlSocketSetting()
        {
            InitializeComponent();
        }
        #endregion

        #region Ui Events
        private void ControlSocketSetting_Load(object sender, EventArgs e)
        {
            textbox_send_buffer_size.Text = SocketSetting.DEFAULT_SEND_BUFFER_SIZE.ToString();
            textbox_send_timeout.Text = SocketSetting.DEFAULT_SEND_TIMEOUT.ToString();
            textbox_riceive_buffer_size.Text = SocketSetting.DEFAULT_RECEIVE_BUFFER_SIZE.ToString();
            textbox_receive_timeout.Text = SocketSetting.DEFAULT_RECEIVE_TIMEOUT.ToString();
            checkbox_no_delay.Checked = SocketSetting.DEFAULT_NO_DELAY;
        }
        private void ButtonDefault_Click(object sender, EventArgs e)
        {
            textbox_send_buffer_size.Text = SocketSetting.DEFAULT_SEND_BUFFER_SIZE.ToString();
            textbox_send_timeout.Text = SocketSetting.DEFAULT_SEND_TIMEOUT.ToString();
            textbox_riceive_buffer_size.Text = SocketSetting.DEFAULT_RECEIVE_BUFFER_SIZE.ToString();
            textbox_receive_timeout.Text = SocketSetting.DEFAULT_RECEIVE_TIMEOUT.ToString();
            checkbox_no_delay.Checked = SocketSetting.DEFAULT_NO_DELAY;
        }
        #endregion

        #region Methods
        public bool TryGetSocketSetting([NotNullWhen(true)] out SocketSetting? setting, [NotNullWhen(false)] out string[]? errors)
        {
            List<string> listError = [];
            if (int.TryParse(textbox_send_buffer_size.Text, out int sendBufferSize))
            {
                if (!SocketSetting.Check_SendBufferSize(sendBufferSize, out string? tmpError))
                    listError.Add(tmpError);
            }
            else
                listError.Add(ERROR_SEND_BUFFER_SIZE_WRONG_FORMAT);
            if (int.TryParse(textbox_send_timeout.Text, out int sendTimeout))
            {
                if (!SocketSetting.Check_SendTimeout(sendTimeout, out string? tmpError))
                    listError.Add(tmpError);
            }
            else
                listError.Add(ERROR_SEND_TIMEOUT_WRONG_FORMAT);
            if (int.TryParse(textbox_riceive_buffer_size.Text, out int receiveBufferSize))
            {
                if (!SocketSetting.Check_ReceiveBufferSize(receiveBufferSize, out string? tmpError))
                    listError.Add(tmpError);
            }
            else
                listError.Add(ERROR_RECEIVE_BUFFER_SIZE_WRONG_FORMAT);
            if (int.TryParse(textbox_receive_timeout.Text, out int receiveTimeout))
            {
                if (!SocketSetting.Check_ReceiveTimeout(receiveTimeout, out string? tmpError))
                    listError.Add(tmpError);
            }
            else
                listError.Add(ERROR_RECEIVE_TIMEOUT_WRONG_FORMAT);
            bool noDelay = checkbox_no_delay.Checked;
            //
            if (listError.Count == 0)
            {
                setting = new(sendBufferSize, sendTimeout, receiveBufferSize, receiveTimeout, noDelay);
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
