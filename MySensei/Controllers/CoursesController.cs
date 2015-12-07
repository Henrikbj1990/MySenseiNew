using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public ActionResult JoinCourse(int courseId)
        {
            var manager = new UserManager<AppUser>(new UserStore<AppUser>(db));
            var currentUser = manager.FindById(User.Identity.GetUserId());

            
            var currentCourse = db.Courses.Where(c => c.CourseID == courseId).FirstOrDefault();


            if (currentUser.Id == currentCourse.CourseTeacherId)
            {
                string sameUserError = "Du kan ikke tilmelde dig til dit eget kursus";
                return View("SingleCourse", sameUserError);
            }


            if (ModelState.IsValid)
            {
                currentCourse.CourseStudents.Add(currentUser);                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }

    }
}