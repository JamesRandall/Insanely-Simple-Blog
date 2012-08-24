using System.Collections.Generic;

namespace InsanelySimpleBlog.ViewModel
{
    public class SidebarViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public IEnumerable<DateTimeIndexViewModel> Indices { get; set; } 
    }
}
