using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace InsanelySimpleBlog.DataModel
{
    public class SimpleBlogDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Settings> Settings { get; set; }

        public SimpleBlogDbContext() : base()
        {
            
        }

        public SimpleBlogDbContext(string sqlConnectionString) : base(sqlConnectionString)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
