﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySensei.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfLessons { get; set; }
        public string AppUserID { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual List<Tag> Tags { get; set; }
    }
}