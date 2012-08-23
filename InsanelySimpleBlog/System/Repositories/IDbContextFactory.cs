using System.Data.Entity;

namespace InsanelySimpleBlog.System.Repositories
{
    internal interface IDbContextFactory
    {
        DbContext CreateContext(string sqlConnectionString);
    }
}
