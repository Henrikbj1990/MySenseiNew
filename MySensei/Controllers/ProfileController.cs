using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using MySensei.Infrastructure;
using MySensei.Models;
using MySensei.ViewModels;

namespace MySensei.Controllers
{

    public class ProfileController : Controller
    {
        private readonly Repository _repository = new Repository();
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Profile
        [Authorize]
        public ActionResult Index()
        {
            //var currentUserId = User.Identity.GetUserId();
            var manager = new UserManager<AppUser>(new UserStore<AppUser>(db));
            var currentUser = manager.FindById(User.Identity.GetUserId());

            return View(currentUser);

        }

        [AllowAnonymous]
        public ActionResult PublicProfile(string profileId)
        {
            var manager = new UserManager<AppUser>(new UserStore<AppUser>(db));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            if (currentUser != null && currentUser.Id == profileId)
            {
                View("Index", currentUser);
            }
            return View(_repository.GetProfileById(profileId));

        }
        [Authorize]
        public async Task<ActionResult> EditProfile()
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
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EditProfile(string id, string firstname, string lastname, string description, string username, string email, string password)
        {
            //var currentUserId = User.Identity.GetUserId();
            var manager = new UserManager<AppUser>(new UserStore<AppUser>(db));
            var currentUser = manager.FindById(User.Identity.GetUserId());

            if (currentUser != null)
            {
                currentUser.FirstName = firstname;
                currentUser.LastName = lastname;
                currentUser.Description = description;
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

        [Authorize]
        public ActionResult AddCourse(string courseTitle)
        {
            ViewBag.courseTitle = courseTitle;
            var course = new Course();
            course.Tags = new List<Tag>();
            PopulateTagsData(course);
            return View(course);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCourse([Bind(Include = "CourseID,Title,Description,StartDate,EndDate,NumberOfLessons,CourseTeacherId")] Course course, string[] selectedTags)
        {
            if (selectedTags != null)
            {
                course.Tags = new List<Tag>();
                foreach (var tag in selectedTags)
                {
                    var tagToAdd = db.Tags.Find(int.Parse(tag));
                    course.Tags.Add(tagToAdd);
                }
            }

            var manager = new UserManager<AppUser>(new UserStore<AppUser>(db));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            course.CourseTeacherId = currentUser.Id;
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }
        [Authorize]
        public ActionResult EditCourse(int? courseId)
        {
            if (courseId == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            //Course course = db.Courses.Find(id);
            Course course = db.Courses
            .Include(c => c.Tags)
            .Where(c => c.CourseID == courseId)
            .Single();
            PopulateTagsData(course);

            if (course == null) { return HttpNotFound(); }

            return View(course);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourse(int? courseId, string[] selectedTags)
        {
            var courseToUpdate = db.Courses.Include(c => c.CourseTeacher).Include(c => c.Tags).Where(c => c.CourseID == courseId).Single();
            var manager = new UserManager<AppUser>(new UserStore<AppUser>(db));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            
            if (TryUpdateModel(courseToUpdate, "", new string[] { "Title", "Description", "StartDate", "EndDate", "NumberOfLessons", "CourseTeacherId" }))
            {
                try
                {
                    UpdateCourseTags(selectedTags, courseToUpdate);
                    db.SaveChanges();
                }
                catch
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            // Tags check boxes
            PopulateTagsData(courseToUpdate);

            return View("Index", currentUser);
        }

        // GET: Courses/Delete/5
        public ActionResult DeleteCourse(int? courseId)
        {
            if (courseId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(courseId);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("DeleteCourse")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int courseId)
        {
            Course course = db.Courses
            .Include(c => c.Tags)
            .Where(c => c.CourseID == courseId)
            .Single();
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
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


        private void PopulateTagsData(Course course)
        {
            var allTags = db.Tags;
            var coursesTags = new HashSet<int>(course.Tags.Select(t => t.TagID));
            var viewModel = new List<AssignedTagData>();
            foreach (var tag in allTags)
            {
                viewModel.Add(new AssignedTagData
                {
                    TagID = tag.TagID,
                    TagName = tag.TagName,
                    Assigned = coursesTags.Contains(tag.TagID)
                });
            }
            ViewBag.Tags = viewModel;
        }


        public void UpdateCourseTags(string[] selectedTags, Course courseToUpdate)
        {
            if (selectedTags == null)
            {
                courseToUpdate.Tags = new List<Tag>();
                return;
            }
            var selectedTagsHS = new HashSet<string>(selectedTags);
            var courseTags = new HashSet<int>(courseToUpdate.Tags.Select(t => t.TagID));
            foreach (var tag in db.Tags)
            {
                if (selectedTagsHS.Contains(tag.TagID.ToString()))
                {
                    if (!courseTags.Contains(tag.TagID))
                    {
                        courseToUpdate.Tags.Add(tag);
                    }
                }
                else
                {
                    if (courseTags.Contains(tag.TagID))
                    {
                        courseToUpdate.Tags.Remove(tag);
                    }
                }
            }
        }
    }
}