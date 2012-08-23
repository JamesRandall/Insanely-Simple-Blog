using System;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace InsanelySimpleBlog.DataModel
{
    public class SimpleBlogInitializer : DropCreateDatabaseIfModelChanges<SimpleBlogDbContext>
    { 
        protected override void Seed(SimpleBlogDbContext context)
        {
            base.Seed(context);
            
            Author author = new Author {Name = "James Randall"};
            context.Authors.Add(author);
            
            Category category = new Category {Name = "Default"};
            context.Categories.Add(category);

            Post post = new Post
                            {
                                Author = author,
                                Categories = new Collection<Category>() { category },
                                Body = "A test post",
                                PostedAt = DateTime.Now,
                                Subject = "A test message"
                            };

            context.Posts.Add(post);
            context.SaveChanges();
        }
    }
}
