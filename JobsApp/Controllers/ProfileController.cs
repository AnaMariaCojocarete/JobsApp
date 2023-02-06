using JobsApp.Data;
using JobsApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace JobsApp.Controllers
{
    public class ProfileController : Controller
    {
        private Repository.ProfileRepository _repository;

        public ProfileController(ApplicationDbContext dbContext)
        {
            _repository = new Repository.ProfileRepository(dbContext);
        }

        [Authorize(Roles = "Candidate, Company, Admin")]
        // GET: ProfileController
        public ActionResult Index()
        {
            var profiles = _repository.GetAllProfiles();
            return View("IndexProfiles", profiles);
        }

        // GET: ProfileController/Details/5
        public ActionResult Details(string id)
        {
            var model = _repository.GetProfileById(id);
            return View("ProfileDetails", model);
        }
        
        [Authorize(Roles = "Candidate, Company, Admin")]
        // GET: ProfileController/Create
        public ActionResult Create()
        {
            var model = new ProfileModel();
            model.Idprofile = User.Identity.Name;
            return View("CreateProfile", model);
        }

        // POST: ProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Candidate, Company, Admin")]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Models.ProfileModel model = new Models.ProfileModel();

                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _repository.InsertProfile(model);
                }

                return View("ProfileDetails", model);
            }
            catch
            {
                return View("CreateProfile");
            }
        }

        // GET: ProfileController/Edit/5
        public ActionResult Edit(string id)
        {
            var model = _repository.GetProfileById(id);
            return View("EditProfile", model);
        }

        // POST: ProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                var model = new ProfileModel();

                var task = TryUpdateModelAsync(model);
                task.Wait();

                if (task.Result)
                {
                    _repository.UpdateProfile(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", id);
                }
            }
            catch
            {
                return RedirectToAction("Index", id);
            }
        }

        // GET: ProfileController/Delete/5
        public ActionResult Delete(string id)
        {
            var model = _repository.GetProfileById(id);
            return View("DeleteProfile", model);
        }

        // POST: ProfileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            try
            {
                var model = _repository.GetProfileById(id);
                _repository.DeleteProfile(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteProfile", id);
            }
        }
    }
}
