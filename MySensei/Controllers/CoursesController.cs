using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySensei.Infrastructure;
using MySensei.Models;

namespace MySensei.Controllers
{
    public class CoursesController : Controller
    {
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
            var course = _repository.GetCourseById(courseId);
            return View(course);
        }

    }
}