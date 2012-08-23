using System;

namespace InsanelySimpleBlog.System.Policies
{
    /// <summary>
    /// Retry policies are used to wrap tasks where temporary failure (transient faults) are expected.
    /// </summary>
    internal interface IRetryPolicy
    {
        void Execute(Action action);
        T Execute<T>(Func<T> func);
    }
}
