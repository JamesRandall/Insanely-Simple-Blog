using System;
using InsanelySimpleBlog.DataModel;
using InsanelySimpleBlog.System.Configuration;
using InsanelySimpleBlog.System.Policies.Implementation;

namespace InsanelySimpleBlog.System.Repositories.Implementation
{
    class EntityFrameworkUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IDbContextFactory _contextFactory;

        public EntityFrameworkUnitOfWorkFactory() : this(new Configuration.Implementation.Configuration(), new SimpleBlogContextFactory())
        {
            
        }

        public EntityFrameworkUnitOfWorkFactory(IConfiguration configuration, IDbContextFactory contextFactory)
        {
            if (contextFactory == null) throw new ArgumentNullException("contextFactory");
            _configuration = configuration;
            _contextFactory = contextFactory;
        }

        public IUnitOfWork Create()
        {
            return new EntityFrameworkUnitOfWork(_configuration, _contextFactory, new SqlAzureRetryPolicy());
        }
    }
}
