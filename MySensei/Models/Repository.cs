﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySensei.Infrastructure;

namespace MySensei.Models
{
    public class Repository
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        
        // GET: Courses
        public IEnumerable<Course> GetCourses()
        {
            return db.Courses.ToList();
        }

        internal Course GetCourseById(int courseId)
        {
            return db.Courses.FirstOrDefault(x => x.CourseID == courseId);
        }

        internal AppUser GetProfileById(string profileId)
        {
            return db.Users.FirstOrDefault(x => x.Id == profileId);
        }

        public IQueryable<Course> SearchCourses(string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                searchString = "";
            }

            return db.Courses.Where(s => s.Title.Contains(searchString));
        }
    }
}