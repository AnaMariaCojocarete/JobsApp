using JobsApp.Data;
using JobsApp.Models;
using JobsApp.Models.DBObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobsApp.Controllers
{
    public class ApplicationController : Controller
    {
        private Repository.ApplicationRepository _repository;
        private Repository.AnnouncementRepository announcementRepository;

        public ApplicationController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.ApplicationRepository(dbContext);
            announcementRepository = new Repository.AnnouncementRepository(dbContext);
        }

        // GET: ApplicationController
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Candidate")]
        public ActionResult MyApplications()
        {
            var myapplications = _repository.GetAllApplicationsByCandidateProfile(User.Identity.Name);
            return View("MyApplications", myapplications);
        }

        [Authorize(Roles = "Company")]
        public ActionResult ApplicationsToAnnouncement()
        {
            if (TempData["announcementApplications"] != null )
            {
                var announcementId = new Guid(TempData["announcementApplications"].ToString());
                var applicationsToAnnouncement = _repository.GetAllApplicationsByAnnouncement(announcementId);
                return View("ApplicationsToAnnouncement", applicationsToAnnouncement);
            }
            else
            {
                var myAnnouncements = announcementRepository.GetAllAnnouncementsByProfile(User.Identity.Name);
                return View("~/Views/Announcement/MyAnnouncements.cshtml", myAnnouncements);
            }    
        }

        // GET: ApplicationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApplicationController/Create
        [Authorize(Roles = "Candidate")]
        public ActionResult Create()
        {
            var model = new ApplicationModel();
            return View("CreateApplication", model);
        }

        // POST: ApplicationController/Create
        [Authorize(Roles = "Candidate")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Models.ApplicationModel model = new Models.ApplicationModel();
                var announcement = new Guid(TempData["announcement"].ToString());
                var cv = new Guid(TempData["cv"].ToString());
                var task = TryUpdateModelAsync(model);
                task.Wait();
                _repository.InsertApplication(model, cv, announcement);
                return View("CreateApplication");
            }
            catch
            {
                return View("CreateApplication");
            }
        }

        // GET: ApplicationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApplicationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ApplicationController/Delete/5
        [Authorize(Roles = "Candidate")]
        public ActionResult Delete(Guid id)
        {
            var model = _repository.GetApplicationById(id);
            return View("Delete", model);
        }

        // POST: ApplicationController/Delete/5
        [Authorize(Roles = "Candidate")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var model = _repository.GetApplicationById(id);
                _repository.DeleteApplication(model);
                return RedirectToAction("MyApplications");
            }
            catch
            {
                return View("Delete", id);
            }
        }
    }
}
