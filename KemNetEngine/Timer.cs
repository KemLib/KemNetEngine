namespace KemNetEngine
{
    public class Timer : ITime
    {
        #region Properties
        private DateTime startup,
            frameTime;
        private int frameCount;
        private float time,
            realTime;
        private float scaleDeltaTime,
            unscaleDeltaTime,
            deltaTime;

        public DateTime Startup => startup;
        public DateTime FrameTime => frameTime;
        public int FrameCount => frameCount;
        public float Time => time;
        public float RealTime => realTime;
        public float ScaleDeltaTime
        {
            get => scaleDeltaTime;
            set => scaleDeltaTime = Math.Max(0, value);
        }
        public float UnScaleDeltaTime => unscaleDeltaTime;
        public float DeltaTime => deltaTime;
        #endregion

        #region Construction
        public Timer()
        {
            Start();
        }
        #endregion

        #region Method
        public void Start()
        {
            startup = DateTime.Now;
            frameTime = startup;
            frameCount = 0;
            time = 0;
            realTime = 0;
            ScaleDeltaTime = 1;
            unscaleDeltaTime = 0;
            deltaTime = unscaleDeltaTime * ScaleDeltaTime;
        }
        public void Update()
        {
            DateTime currentTime = DateTime.Now;
            frameCount++;
            realTime = (float)(currentTime - startup).TotalSeconds;
            unscaleDeltaTime = (float)((currentTime - frameTime).TotalSeconds);
            deltaTime = unscaleDeltaTime * ScaleDeltaTime;
            time += deltaTime;
            realTime += unscaleDeltaTime;
            frameTime = currentTime;
        }
        #endregion
    }
}
