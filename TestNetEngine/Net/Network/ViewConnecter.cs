using KemLibCore.Concurrent.Inter;
using KemNetEngine.Network;
using TestNetEngine.Image;

namespace TestNetEngine.Net.Network
{
    public partial class ViewConnecter : Form
    {
        #region Properties
        private const string LOG_CONNECT_START = "Connect starting...",
            LOG_CONNECT_CANCEL = "Connect cancel!",
            LOG_CONNECT_FAIL = "Connect fail: ",
            LOG_CONNECT_SUCCESS = "Connect success: ";
        private const string TEXT_FORMAT = "Net {0}";

        private InterBoolStruct interConnect;
        private CancellationTokenSource? cancellationTokenSource;
        #endregion

        #region Constructions
        public ViewConnecter()
        {
            InitializeComponent();
            //
            interConnect = new();
        }
        #endregion

        #region Methods
        private void SetState(bool isEnable)
        {
            button_connect.Enabled = isEnable;
            button_cancel.Enabled = !isEnable;
        }
        private static void CancelTokenSource(CancellationTokenSource? cancelToken)
        {
            if (cancelToken == null)
                return;
            //
            try
            {
                cancelToken.Cancel();
            }
            catch (Exception)
            {

            }
            finally
            {
                cancelToken.Dispose();
            }
        }
        #endregion

        #region UI Events
        private void ClientConnectView_Load(object sender, EventArgs e)
        {
            Icon = ResourceFiles.Load_IconKNet();
            SetState(true);
        }
        private void ViewConnecter_FormClosing(object sender, FormClosingEventArgs e)
        {
            Connect_Stop();
        }
        private void Button_Connect_Click(object sender, EventArgs e)
        {
            bool isFail = false;
            if (!control_client_setting.TryGetConnectSetting(out ConnectSetting? connectSetting, out string[]? errors))
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
            if (isFail || connectSetting == null || socketSetting == null)
                return;
            //
            control_list_log.AddLog(LOG_CONNECT_START);
            Connecter connecter = new(connectSetting, socketSetting);
            Text = string.Format(TEXT_FORMAT, connecter.ToString());
            SetState(false);
            Connect_Start(connecter);
        }
        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            control_list_log.AddLog(LOG_CONNECT_CANCEL);
            //
            SetState(true);
            Connect_Stop();
        }
        #endregion

        #region Connect
        private void Connect_Start(Connecter connecter)
        {
            interConnect.Exchange(true);
            cancellationTokenSource = new();
            //
            connecter.Connect(Connect_OnComplete, cancellationTokenSource.Token);
        }
        private void Connect_Stop()
        {
            interConnect.Exchange(false);
            CancelTokenSource(cancellationTokenSource);
            cancellationTokenSource = null;
        }
        private void Connect_OnComplete(ResultConnect result)
        {
            if (IsDisposed)
                return;
            //
            if (interConnect.Value)
            {
                Invoke(() =>
                {
                    if (result.IsSuccess)
                    {
                        string message = LOG_CONNECT_SUCCESS + result.Client.Name;
                        control_list_log.AddLog(message);
                        //
                        Form form = new ViewClient(result.Client)
                        {
                            MdiParent = MdiParent
                        };
                        form.Show();
                    }
                    else
                    {
                        string message = LOG_CONNECT_FAIL + result.ErrorMessage;
                        control_list_log.AddLog(message);
                    }
                    //
                    SetState(true);
                });
            }
            else
            {
                if (result.IsSuccess)
                    result.Client.Close();
            }
        }
        #endregion
    }
}
