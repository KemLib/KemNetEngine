using KemLibCore;
using KemLibCore.Concurrent.Inter;
using KemNetEngine.Network;
using TestNetEngine.Image;

namespace TestNetEngine.Net.Network
{
    public partial class ViewListener : Form
    {
        #region Properties
        private const string SERVER_START = "Server starting...",
            SERVER_START_SUCCESS = "Server started",
            SERVER_START_FAIL = "Server start fail: {0}",
            SERVER_STOP = "Server stop!",
            SERVER_ACCEPT_FAIL = "Server accept fal: {0}";
        private const string TEXT_FORMAT = "Net {0}";

        private readonly ClientManager clientManager;
        private InterBoolStruct interRun;
        private InterObjectStruct<Listener> interListener;
        #endregion

        #region Constructions
        public ViewListener()
        {
            InitializeComponent();
            //
            clientManager = new(Server_OnLog);
            interRun = new();
            interListener = new();
        }
        #endregion

        #region Methods
        private void SetState(bool isEnable)
        {
            button_start.Enabled = isEnable;
            button_stop.Enabled = !isEnable;
        }
        #endregion

        #region UI Events
        private void ClientConnectView_Load(object sender, EventArgs e)
        {
            Icon = ResourceFiles.Load_IconKNet();
            SetState(true);
        }

        private void ViewListener_FormClosing(object sender, FormClosingEventArgs e)
        {
            Server_Stop();
        }
        private void Button_Start_Click(object sender, EventArgs e)
        {
            bool isFail = false;
            if (!control_listen_setting.TryGetListenSetting(out ListenSetting? listenerSetting, out string[]? errors))
            {
                isFail = true;
                foreach (var error in errors)
                    control_list_log.AddLog(error);
            }
            if (!control_socket_setting.TryGetSocketSetting(out SocketSetting? socketSetting, out errors))
            {
                isFail = true;
                foreach (var error in errors)
                    control_list_log.AddLog(error);
            }
            if (isFail || listenerSetting == null || socketSetting == null)
                return;
            //
            control_list_log.AddLog(SERVER_START);
            Listener listener = new(listenerSetting, socketSetting);
            Text = string.Format(TEXT_FORMAT, listener.ToString());
            SetState(false);
            Server_Start(listener);
        }
        private void Button_Stop_Click(object sender, EventArgs e)
        {
            control_list_log.AddLog(SERVER_STOP);
            //
            SetState(true);
            Server_Stop();
        }
        #endregion

        #region Server
        private void Server_Start(Listener listener)
        {
            interRun.Exchange(true);
            //
            Result result = listener.Start();
            if (result.IsSuccess)
            {
                interListener.Exchange(listener);
                clientManager.Start();
                listener.Accept(Server_OnAccept);
                //
                control_list_log.AddLog(SERVER_START_SUCCESS);
            }
            else
            {
                string message = string.Format(SERVER_START_FAIL, result.ErrorMessage);
                control_list_log.AddLog(message);
                //
                interRun.Exchange(false);
                SetState(true);
            }
        }
        private void Server_Stop()
        {
            interRun.Exchange(false);
            Listener? listener = interListener.Value;
            listener?.Stop();
            clientManager.Stop();
        }
        private void Server_OnAccept(ResultConnect result)
        {
            if (interRun.Value)
            {
                if (result.IsSuccess)
                {
                    clientManager.Accept(result.Client);
                    Listener? listener = interListener.Value;
                    listener?.Accept(Server_OnAccept);
                }
                else
                {
                    string message = string.Format(SERVER_ACCEPT_FAIL, result.ErrorMessage);
                    Server_OnLog(message);
                }
            }
            else
            {
                if (result.IsSuccess)
                    result.Client.Close();
            }
        }
        private void Server_OnLog(string message)
        {
            if (IsDisposed)
                return;
            //
            Invoke(() =>
            {
                control_list_log.AddLog(message);
            });
        }
        #endregion
    }
}
