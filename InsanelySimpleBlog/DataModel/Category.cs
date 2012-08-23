using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InsanelySimpleBlog.DataModel
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; } 
    }
}
