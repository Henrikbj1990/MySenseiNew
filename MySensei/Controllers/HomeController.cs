using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySensei.Infrastructure;

namespace MySensei.Controllers
{
    public class HomeController : Controller
    {

        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Home
        public ActionResult Index()
        {
            var course = db.Courses.ToList();

            return View(course);
        }
    }
}