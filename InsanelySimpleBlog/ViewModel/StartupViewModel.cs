using System.Collections.Generic;

namespace InsanelySimpleBlog.ViewModel
{
    public class StartupViewModel
    {
        public IEnumerable<PostViewModel> Posts { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public IEnumerable<DateTimeIndexViewModel> Indices { get; set; } 
    }
}
