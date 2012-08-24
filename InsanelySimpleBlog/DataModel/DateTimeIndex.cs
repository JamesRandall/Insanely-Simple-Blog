using System;
using System.ComponentModel.DataAnnotations;

namespace InsanelySimpleBlog.DataModel
{
    public class DateTimeIndex
    {
        public int DateTimeIndexID { get; set; }

        [Range(1, int.MaxValue)]
        public int NumberOfPosts { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
