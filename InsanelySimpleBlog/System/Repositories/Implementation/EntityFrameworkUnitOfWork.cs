using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using InsanelySimpleBlog.System.Configuration;
using InsanelySimpleBlog.System.Policies;

namespace InsanelySimpleBlog.System.Repositories.Implementation
{
    internal sealed class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly IRetryPolicy _retryPolicy;
        private readonly DbContext _context;

        public EntityFrameworkUnitOfWork(IConfiguration configuration, IDbContextFactory dbContextFactory, IRetryPolicy retryPolicy)
        {
            if (dbContextFactory == null) throw new ArgumentNullException("dbContextFactory");
            if (retryPolicy == null) throw new ArgumentNullException("retryPolicy");
            _retryPolicy = retryPolicy;
            _context = dbContextFactory.CreateContext(configuration.SqlConnectionString);
            // NOTE - we need to wrap opening the connection in a retry policy as this can go wrong (for example SQL Azure
            // can throttle connection opening) but the code looks weird - you can't just grab the exposed connection and
            // open it directly as you then get errors about an entity connection only being creatable with a closed
            // connection.
            // Details of this, and this solution, are explained here:
            // http://blogs.msdn.com/b/diego/archive/2012/01/26/exception-from-dbcontext-api-entityconnection-can-only-be-constructed-with-a-closed-dbconnection.aspx 
            _retryPolicy.Execute(() => ((IObjectContextAdapter)_context).ObjectContext.Connection.Open());
        }

        public IRetryPolicy RetryPolicy
        {
            get { return _retryPolicy; }
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new EntityFrameworkRepository<T>(_context, RetryPolicy);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Execute(Action action)
        {
            _retryPolicy.Execute(action);
        }

        public T2 Execute<T2>(Func<T2> action)
        {
            T2 result = default(T2);
            _retryPolicy.Execute(() => result = action());
            return result;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}