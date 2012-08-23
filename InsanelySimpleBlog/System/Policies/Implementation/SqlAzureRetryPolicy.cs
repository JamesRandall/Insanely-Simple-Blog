using System;
using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling.SqlAzure;
using Microsoft.Practices.TransientFaultHandling;

namespace InsanelySimpleBlog.System.Policies.Implementation
{
    internal class SqlAzureRetryPolicy : IRetryPolicy
    {
        private const int MaxRetries = 10;
        private const int Interval = 100;
        private readonly RetryPolicy<SqlAzureTransientErrorDetectionStrategy> _retryPolicy;
        
        public SqlAzureRetryPolicy()
        {
            _retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(MaxRetries, TimeSpan.FromMilliseconds(Interval));
        }

        public void Execute(Action action)
        {
            _retryPolicy.ExecuteAction(action);
        }

        public T Execute<T>(Func<T> func)
        {
            return _retryPolicy.ExecuteAction(func);
        }
    }
}
