using KemLibCore;
using KemLibCore.Concurrent.Inter;
using KemLibCore.DebugLog;
using KemNetEngine;
using KemNetEngine.Server;
using TestNetEngine.Image;

namespace TestNetEngine.Net.Server
{
    public partial class ViewServer : Form
    {
        #region Properties
        private const string SERVER_START_FAIL = "Server start fail: {0}";
        private const string TEXT_FORMAT = "Net {0}";

        private readonly LogQueue logQueue;
        private readonly ServerHander serverHander;
        private InterBoolStruct interRun;
        private InterObjectStruct<Engine> interServer;
        #endregion

        #region Constructions
        public ViewServer()
        {
            InitializeComponent();
            //
            logQueue = new();
            ILogHander logHander = logQueue.CreateHander();
            serverHander = new(logHander);
            interRun = new();
            interServer = new();
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
        private void ViewServer_Load(object sender, EventArgs e)
        {
            Icon = ResourceFiles.Load_IconKNet();
            SetState(true);
            timer_log_update.Start();
        }
        private void ViewServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Server_Stop();
        }
        private void Button_Start_Click(object sender, EventArgs e)
        {
            bool isFail = false;
            if (!control_server_setting.TryGetServerSetting(out ServerSetting? serverSetting, out string[]? errors))
            {
                isFail = true;
                foreach (var error in errors)
                    control_list_log.AddLog(error);
            }
            ILogHander logHander = logQueue.CreateHander();
            if (isFail || serverSetting == null)
                return;
            //
            ServerEngine server = new(serverSetting, serverHander, logHander);
            Text = string.Format(TEXT_FORMAT, server.Name);
            SetState(false);
            Server_Start(server);
        }
        private void Button_Stop_Click(object sender, EventArgs e)
        {
            SetState(true);
            Server_Stop();
        }

        private void Timer_Log_Update_Tick(object sender, EventArgs e)
        {
            if (!logQueue.TryDequeueAll(out LogData[] logs))
                return;
            //
            foreach (var log in logs)
                control_list_log.AddLog(log.ToString());
        }
        #endregion

        #region Server
        private void Server_Start(Engine server)
        {
            interRun.Exchange(true);
            //
            Result result = EngineRunner.RunTask(server);
            if (result.IsSuccess)
            {
                interServer.Exchange(server);
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
            Engine? listener = interServer.Value;
            listener?.Stop();
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
