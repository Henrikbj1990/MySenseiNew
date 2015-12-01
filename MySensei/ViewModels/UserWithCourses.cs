using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySensei.Models;

namespace MySensei.ViewModels
{
    public class UserWithCourses
    {
        public AppUser AppUser { get; set; }
        public Course Course { get; set; }
        public AssignedTagData AssignedTagData { get; set; }
    }
}