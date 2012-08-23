using System;
using InsanelySimpleBlog.System.Policies;

namespace InsanelySimpleBlog.System.Repositories
{
    internal interface IUnitOfWork : IDisposable
    {
        IRetryPolicy RetryPolicy { get; }
        IRepository<T> GetRepository<T>() where T : class;
        void Save();
        void Execute(Action action);
        T2 Execute<T2>(Func<T2> action);
    }
}
