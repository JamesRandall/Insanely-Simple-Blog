using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsanelySimpleBlog.DataModel
{
    public class Settings
    {
        public int SettingsID { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// TODO: I need to figure out how to get this inside a MediaTypeFormatter in a neat way then this isn't needed
        /// </summary>
        public string BlogPageUrl { get; set; }
    }
}
