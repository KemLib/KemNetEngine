using KemLibCore;

namespace KemNetEngine
{
    public static class EngineRunner
    {
        #region Properties
        private const string ERROR_THREAD_RUN_FAIL = "The thread start fail: ",
            ERROR_TASK_RUN_FAIL = "The task start fail: ";
        public const float DEFAULT_FPS = 30;
        private const int MAX_DELAY_TIME = 1000;
        private const int DEFAULT_SLEEP1_THRESHOLD = 10;
        #endregion

        #region Method
        public static Result RunThread(Engine engine, float fps = DEFAULT_FPS)
        {
            if (!engine.IsRun)
            {
                Result result = engine.Start();
                if (!result.IsSuccess)
                    return result;
            }
            //
            try
            {
                ThreadStart threadStart = new(() =>
                {
                    Run(engine, fps);
                });
                Thread thread = new(threadStart);
                thread.Start();
                //
                return Result.Success();
            }
            catch (Exception ex)
            {
                engine.Stop();
                //
                return Result.Fail(ERROR_THREAD_RUN_FAIL + ex.Message);
            }
        }
        public static Result RunTask(Engine engine, float fps = DEFAULT_FPS, CancellationToken cancellationToken = default)
        {
            if (!engine.IsRun)
            {
                Result result = engine.Start();
                if (!result.IsSuccess)
                    return result;
            }
            //
            try
            {
                _ = Task.Run(async () =>
                {
                    await RunAsync(engine, fps, cancellationToken);
                }, CancellationToken.None);
                //
                return Result.Success();
            }
            catch (Exception ex)
            {
                engine.Stop();
                //
                return Result.Fail(ERROR_TASK_RUN_FAIL + ex.Message);
            }
        }
        #endregion

        #region Run
        public static void Run(Engine engine, float fps)
        {
            if (fps <= 0)
            {
                Run(engine);
                return;
            }
            //
            DateTime currentTime;
            if (engine.IsRun)
            {
                currentTime = DateTime.MinValue;
            }
            else
            {
                currentTime = DateTime.Now;
                Result result = engine.Start();
                if (!result.IsSuccess)
                    return;
            }
            //
            double minDeltaTime = 1000 / fps;
            SpinWait spinWait = new();
            while (engine.IsRun)
            {
                double deltaTime = (DateTime.Now - currentTime).TotalMilliseconds,
                    delayTime = minDeltaTime - deltaTime;
                if (delayTime > 0)
                {
                    try
                    {
                        Thread.Sleep(Math.Min((int)delayTime, MAX_DELAY_TIME));
                    }
                    catch (Exception)
                    {

                    }
                    //
                    continue;
                }
                //
                currentTime = DateTime.Now;
                engine.Update();
                spinWait.SpinOnce(DEFAULT_SLEEP1_THRESHOLD);
            }
            //
            engine.Stop();
        }
        public static void Run(Engine engine)
        {
            if (!engine.IsRun)
            {
                Result result = engine.Start();
                if (!result.IsSuccess)
                    return;
            }
            //
            SpinWait spinWait = new();
            while (engine.IsRun)
            {
                engine.Update();
                spinWait.SpinOnce(DEFAULT_SLEEP1_THRESHOLD);
            }
            //
            engine.Stop();
        }
        public static async Task RunAsync(Engine engine, float fps, CancellationToken cancellationToken = default)
        {
            if (fps <= 0)
            {
                await RunAsync(engine, CancellationToken.None);
                return;
            }
            //
            DateTime currentTime;
            if (engine.IsRun)
            {
                currentTime = DateTime.MinValue;
            }
            else
            {
                currentTime = DateTime.Now;
                Result result = await engine.StartAsync();
                if (!result.IsSuccess)
                    return;
            }
            //
            double minDeltaTime = 1000 / fps;
            while (engine.IsRun && !cancellationToken.IsCancellationRequested)
            {
                double deltaTime = (DateTime.Now - currentTime).TotalMilliseconds,
                    delayTime = minDeltaTime - deltaTime;
                if (delayTime > 0)
                {
                    try
                    {
                        await Task.Delay(Math.Min((int)delayTime, MAX_DELAY_TIME), cancellationToken);
                    }
                    catch (Exception)
                    {

                    }
                    //
                    continue;
                }
                //
                currentTime = DateTime.Now;
                await engine.UpdateAsync();
            }
            //
            await engine.StopAsync();
        }
        public static async Task RunAsync(Engine engine, CancellationToken cancellationToken = default)
        {
            if (!engine.IsRun)
            {
                Result result = await engine.StartAsync();
                if (!result.IsSuccess)
                    return;
            }
            //
            while (engine.IsRun && !cancellationToken.IsCancellationRequested)
                await engine.UpdateAsync();
            //
            await engine.StopAsync();
        }
        #endregion
    }
}
