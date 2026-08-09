namespace KemNetEngine
{
    public interface ITime
    {
        DateTime Startup
        {
            get;
        }
        DateTime FrameTime
        {
            get;
        }
        int FrameCount
        {
            get;
        }
        float Time
        {
            get;
        }
        float RealTime
        {
            get;
        }
        float ScaleDeltaTime
        {
            get;
            set;
        }
        float UnScaleDeltaTime
        {
            get;
        }
        float DeltaTime
        {
            get;
        }
    }
}
