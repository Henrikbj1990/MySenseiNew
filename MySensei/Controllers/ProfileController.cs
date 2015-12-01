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
    public class ProfileController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Profile
        [Authorize]
        public ActionResult Index()
        {
            //var currentUserId = User.Identity.GetUserId();
            var manager = new UserManager<AppUser>(new UserStore<AppUser>(new AppIdentityDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            return View(currentUser);
        }
    }
}