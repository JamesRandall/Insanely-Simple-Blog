namespace InsanelySimpleBlog.System.Threading
{
    internal interface IWaitHandle
    {
        bool Wait(int timeout);
        void Reset();
        bool IsSet { get; }
    }
}
