using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySensei.Models
{
    public class Tag
    {
        public int TagID { get; set; }
        public string TagName { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}