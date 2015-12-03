using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using MySensei.Infrastructure;
using MySensei.Models;

namespace MySensei.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Profile
        
        public ActionResult Index()
        {
            //var currentUserId = User.Identity.GetUserId();
            var manager = new UserManager<AppUser>(new UserStore<AppUser>(db));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            return View(currentUser);
        }

        public async Task<ActionResult> Edit()
        {
            var manager = new UserManager<AppUser>(new UserStore<AppUser>(db));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            if (currentUser != null)
            {
                return View(currentUser);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id, string firstname, string lastname, string username, string email, string password)
        {
            //var currentUserId = User.Identity.GetUserId();
            var manager = new UserManager<AppUser>(new UserStore<AppUser>(db));
            var currentUser = manager.FindById(User.Identity.GetUserId());

            if (currentUser != null)
            {
                currentUser.FirstName = firstname;
                currentUser.LastName = lastname;
                currentUser.UserName = username;
                currentUser.Email = email;
                IdentityResult validEmail = await manager.UserValidator.ValidateAsync(currentUser);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }

                IdentityResult validPass = null;
                if (password != string.Empty)
                {
                    validPass = await manager.PasswordValidator.ValidateAsync(password);
                    if (validPass.Succeeded)
                    {
                        currentUser.PasswordHash = manager.PasswordHasher.HashPassword(password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }

                if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded && password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await manager.UpdateAsync(currentUser);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User Not Found");
                }
            }
            return View();
        }
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
    }
}