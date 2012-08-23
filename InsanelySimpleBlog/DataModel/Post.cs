using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsanelySimpleBlog.DataModel
{
    public class Post
    {
        public int PostID { get; set; }
        public int AuthorID { get; set; }

        [Required]
        [MaxLength(64)]
        public string Subject { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength]
        public string Body { get; set; }

        [Required]
        public DateTime PostedAt { get; set; }

        [Required]
        public Guid ExternalIdentifier { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual Author Author { get; set; } 
    }
}
