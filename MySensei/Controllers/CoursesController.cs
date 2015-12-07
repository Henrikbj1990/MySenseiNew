using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
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
            ViewBag.textBoxString = SearchString;
            return View(_repository.SearchCourses(SearchString).ToList());
        }

        public ActionResult SingleCourse(int courseId)
        {
            var course = _repository.GetCourseById(courseId);
            var manager = new UserManager<AppUser>(new UserStore<AppUser>(db));
            var currentUser = manager.FindById(User.Identity.GetUserId());

            var courseStudentId = course.CourseStudents.ToList();
            if (currentUser == null)
            {
                ViewBag.noLogin = true;
            }
            else
            {
                if (courseStudentId.Any(c => c.Id == currentUser.Id))
                {
                    ViewBag.userIsOnCourse = true;
                }
                else
                {
                    ViewBag.userIsOnCourse = false;
                }

                if (course.CourseTeacherId == currentUser.Id)
                {
                    ViewBag.isTeacher = true;
                }
            }


            return View(course);
        }

        public ActionResult JoinCourse(int courseId)
        {
            var manager = new UserManager<AppUser>(new UserStore<AppUser>(db));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var course = _repository.GetCourseById(courseId);
            
            var currentCourse = db.Courses.FirstOrDefault(c => c.CourseID == courseId);


            if (ModelState.IsValid)
            {
                currentCourse.CourseStudents.Add(currentUser);                
                db.SaveChanges();
                
            }
            return RedirectToAction("SingleCourse", new { courseId = courseId });
        }
        public ActionResult LeaveCourse(int courseId)
        {
            var manager = new UserManager<AppUser>(new UserStore<AppUser>(db));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var course = _repository.GetCourseById(courseId);

            var currentCourse = db.Courses.FirstOrDefault(c => c.CourseID == courseId);


            if (ModelState.IsValid)
            {
                currentCourse.CourseStudents.Remove(currentUser);
                db.SaveChanges();
                
            }
            return RedirectToAction("SingleCourse", new { courseId = courseId });
        }

    }
}