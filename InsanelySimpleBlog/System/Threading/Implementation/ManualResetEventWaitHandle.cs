using System.Threading;

namespace InsanelySimpleBlog.System.Threading.Implementation
{
    class ManualResetEventWaitHandle : IWaitHandle
    {
        private readonly ManualResetEvent _manualResetEvent;

        public ManualResetEventWaitHandle()
        {
            _manualResetEvent = new ManualResetEvent(false);
        }

        public bool Wait(int timeout)
        {
            return _manualResetEvent.WaitOne(timeout);
        }

        public void Reset()
        {
            _manualResetEvent.Reset();
        }

        public bool IsSet
        {
            get { return _manualResetEvent.WaitOne(0); }
        }
    }
}
