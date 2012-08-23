using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using InsanelySimpleBlog.DataModel;

namespace InsanelySimpleBlog.PowerShell
{
    //TODO: Doesn't deal with paths properly as a "first class" PS citizen at the moment
    [Cmdlet(VerbsCommon.Add, "Post")]
    public class AddPost : AbstractDatabaseCmdlet
    {
        [Parameter(Mandatory = true, HelpMessage = "The filename of the markdown file you want to upload. The first line will be taken as the subject title for the post.")]
        public string Filename { get; set; }

        [Parameter(Mandatory = false, HelpMessage = "A comma delimited list of the category names to assign the post to")]
        public string Categories { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            string[] markdownFile = File.ReadAllLines(GetResolvedPath());

            string subject = markdownFile[0];
            while (subject.StartsWith("#"))
            {
                subject = subject.Remove(0, 1);
            }
            subject = subject.Trim();

            StringBuilder bodyBuilder = new StringBuilder();
            for (int lineNumber = 1; lineNumber < markdownFile.Length; lineNumber++)
            {
                bodyBuilder.AppendLine(markdownFile[lineNumber]);
            }
            

            string[] categoryNames = null;
            if (!String.IsNullOrWhiteSpace(Categories))
            {
                categoryNames = Categories.Split(',');
            }

            using (SimpleBlogDbContext context = new SimpleBlogDbContext(GetConnectionString()))
            {
                ICollection<Category> categories = GetCategories(categoryNames, context);

                int authorId = context.Authors.First().AuthorID;
                Post newPost = new Post
                                   {
                                       AuthorID = authorId,
                                       Body = bodyBuilder.ToString(),
                                       Categories = categories,
                                       ExternalIdentifier = Guid.NewGuid(),
                                       Subject = subject,
                                       PostedAt = DateTime.UtcNow
                                   };
                context.Posts.Add(newPost);
                context.SaveChanges();
            }
        }

        private string GetResolvedPath()
        {
            ProviderInfo provider;
            PSDriveInfo drive;

            string path = this.SessionState.Path.GetUnresolvedProviderPathFromPSPath(Filename, out provider, out drive);
            return path;
        }

        private static ICollection<Category> GetCategories(IEnumerable<string> categoryNames, SimpleBlogDbContext context)
        {
            if (categoryNames == null || !categoryNames.Any())
            {
                return null;
            }

            List<Category> categories = new List<Category>();
            foreach (string categoryName in categoryNames)
            {
                Category category =
                    context.Categories.SingleOrDefault(x => x.Name.Equals(categoryName, StringComparison.InvariantCultureIgnoreCase));
                if (category == null)
                {
                    category = new Category
                                   {
                                       Name = categoryName
                                   };
                }
                categories.Add(category);
            }
            return categories;
        }
    }
}
