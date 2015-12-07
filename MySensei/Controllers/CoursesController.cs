using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySensei.Infrastructure;
using MySensei.Models;

namespace MySensei.Controllers
{
    public class CoursesController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        private readonly Repository _repository = new Repository();

        // GET: Home
        public ActionResult Index()
        {
            return View(_repository.GetCourses());
        }

        [HttpPost]
        public ActionResult SearchCourses(string SearchString)
        {
            return View(_repository.SearchCourses(SearchString).ToList());
        }

        public ActionResult SingleCourse(int courseId)
        {
            return View(_repository.GetCourseById(courseId));
        }

        [HttpPost]
        public ActionResult JoinCourse(int courseId)
        {
            var manager = new UserManager<AppUser>(new UserStore<AppUser>(db));
            var currentUser = manager.FindById(User.Identity.GetUserId());

            return View("SingleCourse", courseId);
        }

    }
}