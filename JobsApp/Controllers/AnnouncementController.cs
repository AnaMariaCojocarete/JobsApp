using JobsApp.Data;
using JobsApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JobsApp.Controllers
{
    public class AnnouncementController : Controller
    {
        private Repository.AnnouncementRepository _repository;

        public AnnouncementController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.AnnouncementRepository(dbContext);
        }

        // GET: AnnouncementController
        public ActionResult Index()
        {
            var announcements = _repository.GetAllAnnouncements();
            return View("IndexAnnouncement", announcements);
        }

        [Authorize(Roles = "Company")]
        public ActionResult MyAnnouncements()
        {
            var announcements = _repository.GetAllAnnouncementsByProfile(User.Identity.Name);
            return View("MyAnnouncements", announcements);
        }

        // GET: AnnouncementController/Details/5
        public ActionResult Details(Guid id)
        {
            TempData["announcement"] = id;
            TempData["announcementApplications"] = id;
            var model = _repository.GetAnnouncementById(id);
            return View("AnnouncementDetails", model);
        }

        // GET: AnnouncementController/Create
        [Authorize(Roles = "Company")]
        public ActionResult Create()
        {
            var model = new AnnouncementModel();
            return View("CreateAnnouncement", model);
        }

        // POST: AnnouncementController/Create
        [Authorize(Roles = "Company")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                AnnouncementModel model = new AnnouncementModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                _repository.InsertAnnouncement(model, User.Identity.Name);
                return View("CreateAnnouncement");
            }
            catch
            {
                return View("CreateAnnouncement");
            }
        }

        // GET: AnnouncementController/Edit/5
        [Authorize(Roles = "Company")]
        public ActionResult Edit(Guid id)
        {
            var model = _repository.GetAnnouncementById(id);
            return View("EditAnnouncement", model);
        }

        // POST: AnnouncementController/Edit/5
        [Authorize(Roles = "Company")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var model = new AnnouncementModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                _repository.UpdateAnnouncement(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index", id);
            }
        }

        // GET: AnnouncementController/Delete/5
        [Authorize(Roles = "Company")]
        public ActionResult Delete(Guid id)
        {
            var model = _repository.GetAnnouncementById(id);
            return View("DeleteAnnouncement", model);
        }

        // POST: AnnouncementController/Delete/5
        [Authorize(Roles = "Company")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var model = _repository.GetAnnouncementById(id);
                _repository.DeleteAnnouncement(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteAnnouncement", id);
            }
        }
    }
}
