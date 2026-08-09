using KemLibCore;
using KemLibCore.Concurrent.Inter;
using KemLibCore.Concurrent.Queue;
using KemLibCore.DebugLog;
using System.Net;
using System.Net.Sockets;

namespace KemNetEngine.Server
{
    public class ServerEngine : Engine
    {
        #region Properties
        private const string LOG_STARTING = "Server starting...",
            LOG_STARTED = "Server started!",
            LOG_STOPPING = "Server stopping...",
            LOG_STOPED = "Server stoped!";
        private const string SERVER_START_LISTEN_START_FAIL = "Server start listen fail";
        private const string LOG_CALL_ON_START_FAIL = "Call method Server_OnStart fail",
            LOG_CALL_ON_UPDATE_FAIL = "Call method Server_OnUpdate fail",
            LOG_CALL_ON_STOP_FAIL = "Call method Server_OnStop fail",
            LOG_CALL_ON_PLAYER_CONNECT_FAIL = "Call method Server_OnPlayerConnect fail",
            LOG_EVENT_ACTION_FAIL_TITLE = "Event action fail";
        private const string NAME_FOTMAT = "ServerEngine({0})";

        public readonly ServerSetting Setting;
        private readonly IServerHander serverHander;
        public readonly string Name;
        private readonly ListenManager listenManager;
        public readonly PlayerManager PlayerManager;
        private readonly ILogHander logHander;
        private readonly TwiceQueue<IEvent> queueEvent;
        private readonly InterObjectStruct<IQueueIn<IEvent>> interQueueInvoke;
        #endregion

        #region Construction
        public ServerEngine(ServerSetting setting, IServerHander serverHander, ILogHander logHander) : base()
        {
            Setting = setting;
            this.serverHander = serverHander;
            Name = GetName(setting.Listen.EndPoint);
            this.logHander = logHander;
            listenManager = new(this);
            PlayerManager = new(this);
            queueEvent = new();
            interQueueInvoke = new();
        }
        #endregion

        #region Dispose
        protected override void OnDispose(bool disposing)
        {
            logHander.DisableLog();
            if (disposing)
            {

            }
        }
        #endregion

        #region Method
        private static string GetName(IPEndPoint localEndPoint)
        {
            return string.Format(NAME_FOTMAT, localEndPoint.ToString());
        }
        public override string ToString()
        {
            return Name;
        }
        #endregion

        #region Engine
        protected override Result OnStart()
        {
            logHander.LogMessage(Name, LOG_STARTING);
            //
            IQueueIn<IEvent>? queueInvoke = Event_CreateQueue();
            if (listenManager.TryStart(out string? error))
            {
                interQueueInvoke.Exchange(queueInvoke);
                Hander_Start();
                //
                logHander.LogMessage(Name, LOG_STARTED);
                return Result.Success();
            }
            else
            {
                queueInvoke.Disable();
                queueEvent.Clear();
                //
                logHander.LogError(Name, SERVER_START_LISTEN_START_FAIL, error);
                return Result.Fail(SERVER_START_LISTEN_START_FAIL);
            }
        }
        protected override void OnUpdate()
        {
            PlayerManager.Player_Update();
            Event_Action();
            PlayerManager.Player_Remove();
            Hander_Update();
            //
            Listen_Accept();
            listenManager.RequestClient = Setting.GetRequestClientPerFrame(PlayerManager.Count);
        }
        protected override void OnStop()
        {
            logHander.LogMessage(Name, LOG_STOPPING);
            //
            PlayerManager.Player_Stoping();
            listenManager.Stoping();
            //
            if (PlayerManager.Count == 0)
            {
                Event_Action();
            }
            else
            {
                SpinWait spinWait = new();
                do
                {
                    PlayerManager.Player_StopUpdate();
                    Event_Action();
                    PlayerManager.Player_Remove();
                    //
                    spinWait.SpinOnce();
                } while (PlayerManager.Count > 0);
            }
            //
            listenManager.Stop();
            //
            IQueueIn<IEvent>? queueInvoke = interQueueInvoke.Exchange(null);
            queueInvoke?.Disable();
            Event_ActionClear();
            //
            Hander_Stop();
            logHander.LogMessage(Name, LOG_STOPED);
        }
        protected override async Task<Result> OnStartAsync()
        {
            await logHander.LogMessageAsync(Name, LOG_STARTING);
            //
            IQueueIn<IEvent> queueInvoke = await Event_CreateQueueAsync();
            if (listenManager.TryStart(out string? error))
            {
                interQueueInvoke.Exchange(queueInvoke);
                Hander_Start();
                //
                await logHander.LogMessageAsync(Name, LOG_STARTED);
                return Result.Success();
            }
            else
            {
                queueInvoke.Disable();
                await queueEvent.ClearAsync();
                //
                await logHander.LogErrorAsync(Name, SERVER_START_LISTEN_START_FAIL, error);
                return Result.Fail(SERVER_START_LISTEN_START_FAIL);
            }
        }
        protected override async Task OnUpdateAsync()
        {
            PlayerManager.Player_Update();
            await Event_ActionAsync();
            PlayerManager.Player_Remove();
            Hander_Update();
            //
            await Listen_AcceptAsync();
            listenManager.RequestClient = Setting.GetRequestClientPerFrame(PlayerManager.Count);
        }
        protected override async Task OnStopAsync()
        {
            await logHander.LogMessageAsync(Name, LOG_STOPPING);
            //
            PlayerManager.Player_Stoping();
            await listenManager.StopingAsync();
            //
            if (PlayerManager.Count == 0)
            {
                await Event_ActionAsync();
            }
            else
            {
                do
                {
                    PlayerManager.Player_StopUpdate();
                    await Event_ActionAsync();
                    PlayerManager.Player_Remove();
                } while (PlayerManager.Count > 0);
            }
            //
            await listenManager.StopAsync();
            //
            IQueueIn<IEvent>? queueInvoke = interQueueInvoke.Exchange(null);
            queueInvoke?.Disable();
            await Event_ActionClearAsync();
            //
            Hander_Stop();
            await logHander.LogMessageAsync(Name, LOG_STOPED);
        }
        #endregion

        #region Listen
        private void Listen_Accept()
        {
            Socket[] arrayClient = listenManager.Client_GetAll();
            PlayerManager.Player_Accept(arrayClient, logHander);
        }
        private async Task Listen_AcceptAsync()
        {
            Socket[] arrayClient = await listenManager.Client_GetAllAsync();
            await PlayerManager.Player_AcceptAsync(arrayClient, logHander);
        }
        #endregion

        #region Event
        internal IQueueIn<IEvent> Event_CreateQueue()
        {
            return queueEvent.CreateQueueIn();
        }
        internal async Task<IQueueIn<IEvent>> Event_CreateQueueAsync()
        {
            return await queueEvent.CreateQueueInAsync();
        }
        private void Event_Action()
        {
            if (queueEvent.TryDequeueAll(out IEvent[]? eventArray))
                Event_Action(eventArray);
        }
        private void Event_ActionClear()
        {
            if (queueEvent.TryClear(out IEvent[]? eventArray))
                Event_Action(eventArray);
        }
        private async Task Event_ActionAsync()
        {
            IEvent[] eventArray = await queueEvent.DequeueAllAsync();
            Event_Action(eventArray);
        }
        private async Task Event_ActionClearAsync()
        {
            IEvent[] eventArray = await queueEvent.ClearAsync();
            Event_Action(eventArray);
        }
        private void Event_Action(IEvent[] arrayEvent)
        {
            foreach (IEvent item in arrayEvent)
            {
                try
                {
                    item.Run();
                }
                catch (Exception ex)
                {
                    logHander.LogWarning(Name, LOG_EVENT_ACTION_FAIL_TITLE, ex.Message);
                }
            }
        }
        #endregion

        #region Hander
        private void Hander_Start()
        {
            try
            {
                serverHander.Server_OnStart(this);
            }
            catch (Exception ex)
            {
                logHander.LogWarning(Name, LOG_CALL_ON_START_FAIL, ex.Message);
            }
        }
        private void Hander_Update()
        {
            try
            {
                serverHander.Server_OnUpdate(this);
            }
            catch (Exception ex)
            {
                logHander.LogWarning(Name, LOG_CALL_ON_UPDATE_FAIL, ex.Message);
            }
        }
        private void Hander_Stop()
        {
            try
            {
                serverHander.Server_OnStop(this);
            }
            catch (Exception ex)
            {
                logHander.LogWarning(Name, LOG_CALL_ON_STOP_FAIL, ex.Message);
            }
        }
        internal bool Hander_PlayerConnect(PlayerControl player, [NotNullWhen(true)] out IPlayerHander? playerHander)
        {
            try
            {
                playerHander = serverHander.Server_OnPlayerConnect(this, player);
                if (playerHander == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                logHander.LogWarning(Name, LOG_CALL_ON_PLAYER_CONNECT_FAIL, ex.Message);
                //
                playerHander = null;
                return false;
            }
        }
        #endregion

        #region Invoke
        public override bool TryInvoke(IEvent action)
        {
            if (action == null)
                return false;
            IQueueIn<IEvent>? queueInvoke = interQueueInvoke.Value;
            if (queueInvoke == null)
                return false;
            //
            return queueInvoke.TryEnqueue(action);
        }
        public override async Task<bool> TryInvokeAsync(IEvent action)
        {
            if (action == null)
                return false;
            IQueueIn<IEvent>? queueInvoke = interQueueInvoke.Value;
            if (queueInvoke == null)
                return false;
            //
            return await queueInvoke.TryEnqueueAsync(action);
        }
        #endregion
    }
}
