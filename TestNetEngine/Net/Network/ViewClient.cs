using KemLibCore.Concurrent.Inter;
using KemNetEngine.Network;
using System.Net.Sockets;

namespace TestNetEngine.Net.Network
{
    public partial class ViewClient : Form
    {
        #region Properties
        private const string LOG_CLICK_CLOSE = "Click close",
            LOG_CLOSED = "Client closed: {0}",
            LOG_SEND_BEGIN = "Client send begin",
            LOG_SEND_END = "Client send end {0} message: {1}",
            LOG_SEND_COMPLETE = "Client send complete {0} message: {1}",
            LOG_SEND_FAIL = "Client send message index[{0}] fail: {1}",
            LOG_RECEIVE_MESSAGE_FORMAT_FAIL = "Client receive message fomat fai: {0}",
            LOG_RECEIVE_FAIL = "Client receive fai: {0}",
            LOG_RECEIVE_END = "Client receive end {0}";
        private const string MESSAGE_FORMAT = "{0} {1}: {2}";
        private const string TEXT_FORMAT = "Net {0}",
            NAME_FORMAT = "{0}_{1}";

        private readonly IClient client;
        private InterBoolStruct interConnect;
        private readonly MessageManager messageManager;
        private DateTime timeStartSend;
        #endregion

        #region Construction
        public ViewClient()
        {
            InitializeComponent();
            //
            client = IClient.ClientNone;
            interConnect = new();
            messageManager = new MessageManager();
        }
        public ViewClient(IClient client)
        {
            InitializeComponent();
            //
            this.client = client;
            interConnect = new();
            messageManager = new MessageManager();
            //
            this.client.OnDisconnect += Client_OnDisconnect;
            this.client.Receive(Client_OnReceive);
        }
        #endregion

        #region Methods
        private void SetState(bool isConnect)
        {
            button_send.Enabled = isConnect;
            button_disconnect.Enabled = isConnect;
            panel_send.Enabled = isConnect;
        }
        private void RandomName()
        {
            Random rad = new();
            combobox_send_name.SelectedIndex = rad.Next(0, combobox_send_name.Items.Count);
        }
        #endregion

        #region UI Events
        private void ViewClient_Load(object sender, EventArgs e)
        {
            Text = string.Format(TEXT_FORMAT, client.Name);
            if (client.IsConnect)
            {
                interConnect.Exchange(true);
                SetState(true);
            }
            else
            {
                interConnect.Exchange(false);
                SetState(false);
            }
            //
            RandomName();
            combobox_send_count.SelectedIndex = 0;
            combobox_send_length.SelectedIndex = 0;
        }
        private void ViewClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            Client_Close();
        }
        private void Button_Disconnect_Click(object sender, EventArgs e)
        {
            control_list_log.AddLog(LOG_CLICK_CLOSE);
            Client_Close();
        }
        private void Button_Send_Random_Name_Click(object sender, EventArgs e)
        {
            RandomName();
        }
        private void Button_Send_Click(object sender, EventArgs e)
        {
            string name = string.Format(NAME_FORMAT, combobox_send_name.Text, combobox_send_name.SelectedIndex + 1);
            if (!int.TryParse(combobox_send_count.Text, out int count))
                count = 1;
            if (!int.TryParse(combobox_send_length.Text, out int length))
                length = 1;
            Client_Send(name, count, length);
        }
        #endregion

        #region Client
        private void Client_Close()
        {
            if (!interConnect.TryExchange(false))
                return;
            //
            messageManager.Close();
            SetState(false);
            string message = string.Format(LOG_CLOSED, client.CloseFrom);
            control_list_log.AddLog(message);
            //
            client.Close();
        }
        private void Client_Send(string name, int number, int length)
        {
            if (!client.IsConnect)
                return;
            //
            control_list_log.AddLog(LOG_SEND_BEGIN);
            button_send.Enabled = false;
            //
            List<MessageData> listMessage = messageManager.Send_Begin(name, number, length);
            List<byte[]> listData = new(listMessage.Count);
            foreach (var message in listMessage)
            {
                string json = message.ToJson();
                byte[] data = MessageManager.UTF8_ToByte(json);
                listData.Add(data);
            }
            //
            timeStartSend = DateTime.Now;
            for (int i = 0; i < listData.Count; i++)
                client.Send(listData[i], Client_OnSend, listMessage[i]);
            TimeSpan timeSend = DateTime.Now - timeStartSend;
            control_list_log.AddLog(string.Format(LOG_SEND_END, number, timeSend.TotalSeconds));
        }
        private void Client_OnDisconnect(ClientControl client)
        {
            if (IsDisposed)
                return;
            //
            Invoke(() =>
            {
                Client_Close();
            });
        }
        private void Client_OnSend(ResultSend result, MessageData messageData)
        {
            if (IsDisposed || !interConnect.Value)
                return;
            //
            Invoke(() =>
            {
                if (messageManager.Send_Complete(messageData))
                {
                    button_send.Enabled = true;
                    TimeSpan timeSend = DateTime.Now - timeStartSend;
                    control_list_log.AddLog(string.Format(LOG_SEND_COMPLETE, messageData.MaxIndex + 1, timeSend.TotalSeconds));
                }
                //
                if (result.IsSuccess)
                {

                }
                else
                {
                    if (result.ErrorCode != SocketError.NotConnected && result.ErrorCode != SocketError.ConnectionReset)
                    {
                        control_list_log.AddLog(string.Format(LOG_SEND_FAIL, messageData.Index, result.ErrorMessage));
                    }
                }
            });
        }
        private void Client_OnReceive(ResultReceive result)
        {
            if (IsDisposed || !interConnect.Value)
                return;
            //
            Invoke(() =>
            {
                if (result.IsSuccess)
                {
                    string json = MessageManager.UTF8_ToText(result.Data);
                    if (messageManager.Receive_Complete(json, out MessageData? messageData, out bool isEnd, out string? error))
                    {
                        control_list_message.AddLog(string.Format(MESSAGE_FORMAT, messageData.Value.Index, messageData.Value.User, messageData.Value.Message));
                        //
                        if (isEnd)
                            control_list_log.AddLog(string.Format(LOG_RECEIVE_END, messageData.Value.MaxIndex + 1));
                    }
                    else
                    {
                        control_list_log.AddLog(string.Format(LOG_RECEIVE_MESSAGE_FORMAT_FAIL, error));
                    }
                }
                else
                {
                    if (result.ErrorCode != SocketError.NotConnected && result.ErrorCode != SocketError.ConnectionReset)
                    {
                        control_list_log.AddLog(string.Format(LOG_RECEIVE_FAIL, result.ErrorMessage));
                    }
                    //
                    Client_Close();
                }
            });
            //
            if (result.IsSuccess && client.IsConnect)
                client.Receive(Client_OnReceive);
        }
        #endregion
    }
}
