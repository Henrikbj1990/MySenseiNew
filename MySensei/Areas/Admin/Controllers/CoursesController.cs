using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MySensei.Infrastructure;
using MySensei.Models;
using MySensei.ViewModels;

namespace MySensei.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class CoursesController : Controller
    {
        private AppIdentityDbContext db = new AppIdentityDbContext();

        // GET: Courses
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.CourseTeacher);
            return View(courses.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }


        // GET: Courses/Create
        public ActionResult Create()
        {
            var course = new Course();
            course.Tags = new List<Tag>();
            PopulateTagsData(course);
            return View(course);
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,Title,Description,StartDate,EndDate,NumberOfLessons,CourseTeacherId")] Course course, string[] selectedTags)
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
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }



        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        private AppRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<AppRoleManager>();
            }
        }
        

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            //Course course = db.Courses.Find(id);
            Course course = db.Courses
            .Include(c => c.Tags)
            .Where(c => c.CourseID == id)
            .Single();
            PopulateTagsData(course);

            if (course == null) { return HttpNotFound(); }



            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? CourseID, string[] selectedTags)
        {
            var courseToUpdate = db.Courses.Include(c => c.CourseTeacher).Include(c => c.Tags).Where(c => c.CourseID == CourseID).Single();
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

            // Instructors dropdown list
            // Tags check boxes
            PopulateTagsData(courseToUpdate);
            return View(courseToUpdate);

        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses
            .Include(c => c.Tags)
            .Where(c => c.CourseID == id)
            .Single();
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
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


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
