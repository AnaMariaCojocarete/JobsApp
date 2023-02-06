using JobsApp.Data;
using JobsApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JobsApp.Controllers
{
    public class CvController : Controller
    {
        private Repository.CVRepository _repository;

        public CvController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.CVRepository(dbContext);
        }

        // GET: CvController
        public ActionResult Index()
        {
            var cvs = _repository.GetAllCVsByProfile(User.Identity.Name);
            return View("IndexCv", cvs);
        }

        public ActionResult ApplyList()
        {
            var cvs = _repository.GetAllCVsByProfile(User.Identity.Name);
            return View("ApplyList", cvs);
        }

        public ActionResult ApplyDetails(Guid id)
        {
            TempData["cv"] = id;
            var model = _repository.GetCVById(id);
            return View("ApplyDetails", model);
        }

        // GET: CvController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _repository.GetCVById(id);
            return View("CvDetails", model);
        }

        // GET: CvController/Create
        [Authorize(Roles = "Candidate")]
        public ActionResult Create()
        {
            var model = new CVModel();
            model.Mail = User.Identity.Name;
            return View("CreateCv", model);
        }


        // POST: CvController/Create
        [Authorize(Roles = "Candidate")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Models.CVModel model = new Models.CVModel();

                var task = TryUpdateModelAsync(model);
                task.Wait();
                _repository.InsertCV(model, User.Identity.Name);
                return View("CreateCv");
            }
            catch
            {
                return View("CreateCv");
            }
        }

        // GET: CvController/Edit/5
        [Authorize(Roles = "Candidate")]
        public ActionResult Edit(Guid id)
        {
            var model = _repository.GetCVById(id);
            return View("EditCv", model);
        }

        // POST: CvController/Edit/5
        [Authorize(Roles = "Candidate")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var model = new CVModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                
                _repository.UpdateCV(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index", id);
            }
        }

        // GET: CvController/Delete/5
        [Authorize(Roles = "Candidate")]
        public ActionResult Delete(Guid id)
        {
            var model = _repository.GetCVById(id);
            return View("DeleteCv", model);
        }

        // POST: CvController/Delete/5
        [Authorize(Roles = "Candidate")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var model = _repository.GetCVById(id);
                _repository.DeleteCV(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteCv", id);
            }
        }
    }
}
