using System;
using System.Collections.Generic;

namespace InsanelySimpleBlog.ViewModel
{
    public class PostViewModel
    {
        public int PostID { get; set; }

        public string Subject { get; set; }

        public string BodyAsHtml { get; set; }

        public DateTime PostedAt { get; set; }

        public int AuthorId { get; set; }

        public string AuthorName { get; set; }

        public Guid ExternalIdentifier { get; set; }

        public List<CategoryViewModel> Categories { get; set; } 
    }
}
