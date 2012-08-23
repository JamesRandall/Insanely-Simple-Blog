using System.Data.Entity;
using System.Management.Automation;
using InsanelySimpleBlog.DataModel;

namespace InsanelySimpleBlog.PowerShell
{
    [Cmdlet(VerbsCommon.New, "Deployment")]
    public class NewDeployment : AbstractDatabaseCmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "The name of the default author")]
        public string Author { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "The title of the blog")]
        public string BlogName { get; set; }

        [Parameter(Mandatory = true, HelpMessage = "The URL of the page that hosts the blog")]
        public string BlogPageUrl { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            using (SimpleBlogDbContext context = new SimpleBlogDbContext(GetConnectionString()))
            {
                CreateDatabaseIfNotExists<SimpleBlogDbContext> databaseInitializer = new CreateDatabaseIfNotExists<SimpleBlogDbContext>();
                databaseInitializer.InitializeDatabase(context);

                Author author = new Author
                                    {
                                        Name = Author
                                    };
                context.Authors.Add(author);

                Settings settings = new Settings
                                        {
                                            BlogPageUrl = BlogPageUrl,
                                            Name = BlogName
                                        };
                context.Settings.Add(settings);

                context.SaveChanges();
            }
        }
    }
}
