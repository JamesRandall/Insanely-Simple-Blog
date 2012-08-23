using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsanelySimpleBlog.ViewModel
{
    public class SettingsViewModel
    {
        public string Name { get; set; }

        /// <summary>
        /// TODO: I need to figure out how to get this inside a MediaTypeFormatter in a neat way then this isn't needed
        /// </summary>
        public string BlogPageUrl { get; set; }
    }
}
