using System.Data.Entity;
using InsanelySimpleBlog.System.Repositories;

namespace InsanelySimpleBlog.DataModel
{
    internal class SimpleBlogContextFactory : IDbContextFactory
    {
        public DbContext CreateContext(string sqlConnectionString)
        {
            return new SimpleBlogDbContext(sqlConnectionString);
        }
    }
}