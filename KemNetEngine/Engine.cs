using KemLibCore;
using KemLibCore.Concurrent.Inter;

namespace KemNetEngine
{
    public abstract class Engine : IDisposable
    {
        private enum StateEnum
        {
            None,
            Run,
            Stopping
        }
        #region Properties
        private const string ERROR_IS_RUNED = "The Engine has been running",
            ERROR_IS_DISPOSED = "The Engine has been disposed",
            ERROR_GET_FRAME_FAIL = "Failed to access frame";
        private const int STATE_NONE = (int)StateEnum.None,
            STATE_RUN = (int)StateEnum.Run,
            STATE_STOPPING = (int)StateEnum.Stopping;

        private readonly Timer timer;
        private InterIntStruct interState;
        private InterBoolStruct interDispose;
        private InterBoolStruct interFrame;

        public bool IsRun => interState.Value != STATE_NONE;
        public bool IsStopping => interState.Value == STATE_STOPPING;
        public bool IsDisposed => interDispose.Value;
        public ITime Time => timer;
        #endregion

        #region Construction
        public Engine()
        {
            timer = new();
            interState = new(STATE_NONE);
            interDispose = new();
            interFrame = new();
        }
        ~Engine()
        {
            Dispose(false);
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!interDispose.TryExchange(true))
                return;
            //
            Stop();
            if (disposing)
            {

            }
            OnDispose(disposing);
        }
        protected abstract void OnDispose(bool disposing);
        #endregion

        #region Engine
        public Result Start()
        {
            if (IsDisposed)
                return Result.Fail(ERROR_IS_DISPOSED);
            //
            if (!interFrame.TryExchange(true))
                return Result.Fail(ERROR_GET_FRAME_FAIL);
            //
            try
            {
                if (interState.TryCompareExchange(STATE_RUN, STATE_NONE))
                {
                    timer.Start();
                    Result result = OnStart();
                    if (!result.IsSuccess)
                        interState.CompareExchange(STATE_NONE, STATE_RUN);
                    return result;
                }
                else
                {
                    return Result.Fail(ERROR_IS_RUNED);
                }
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
            finally
            {
                interFrame.Value = false;
            }
        }
        public void Update()
        {
            if (IsDisposed)
                return;
            if (!IsRun)
            {
                if (IsStopping)
                    Stop();
                return;
            }
            //
            if (!interFrame.TryExchange(true))
                return;
            //
            try
            {
                timer.Update();
                OnUpdate();
            }
            catch (Exception)
            {

            }
            finally
            {
                interFrame.Value = false;
            }
            //
            if (IsStopping)
                Stop();
        }
        public void Stop()
        {
            interState.CompareExchange(STATE_STOPPING, STATE_RUN);
            if (!IsStopping)
                return;
            //
            if (!interFrame.TryExchange(true))
                return;
            //
            try
            {
                timer.Update();
                OnStop();
                //
                interState.CompareExchange(STATE_NONE, STATE_STOPPING);
            }
            catch (Exception)
            {

            }
            finally
            {
                interFrame.Value = false;
            }
        }
        public async Task<Result> StartAsync()
        {
            if (IsDisposed)
                return Result.Fail(ERROR_IS_DISPOSED);
            //
            if (!interFrame.TryExchange(true))
                return Result.Fail(ERROR_GET_FRAME_FAIL);
            //
            try
            {
                if (interState.TryCompareExchange(STATE_RUN, STATE_NONE))
                {
                    timer.Start();
                    Result result = await OnStartAsync();
                    if (!result.IsSuccess)
                        interState.CompareExchange(STATE_NONE, STATE_RUN);
                    return result;
                }
                else
                {
                    return Result.Fail(ERROR_IS_RUNED);
                }
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
            finally
            {
                interFrame.Value = false;
            }
        }
        public async Task UpdateAsync()
        {
            if (IsDisposed)
                return;
            if (!IsRun)
            {
                if (IsStopping)
                    await StopAsync();
                return;
            }
            //
            if (!interFrame.TryExchange(true))
                return;
            //
            try
            {
                timer.Update();
                await OnUpdateAsync();
            }
            catch (Exception)
            {

            }
            finally
            {
                interFrame.Value = false;
            }
            //
            if (IsStopping)
                await StopAsync();
        }
        public async Task StopAsync()
        {
            interState.CompareExchange(STATE_STOPPING, STATE_RUN);
            if (!IsStopping)
                return;
            //
            if (!interFrame.TryExchange(true))
                return;
            //
            try
            {
                timer.Update();
                await OnStopAsync();
                //
                interState.CompareExchange(STATE_NONE, STATE_STOPPING);
            }
            catch (Exception)
            {

            }
            finally
            {
                interFrame.Value = false;
            }
        }
        #endregion

        #region Method
        protected abstract Result OnStart();
        protected abstract void OnUpdate();
        protected abstract void OnStop();
        protected abstract Task<Result> OnStartAsync();
        protected abstract Task OnUpdateAsync();
        protected abstract Task OnStopAsync();
        #endregion

        #region Invoke
        public abstract bool TryInvoke(IEvent action);
        public abstract Task<bool> TryInvokeAsync(IEvent action);
        #endregion
    }
}
