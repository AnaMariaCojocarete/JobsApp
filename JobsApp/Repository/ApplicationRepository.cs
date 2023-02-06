using JobsApp.Data;
using JobsApp.Models;
using JobsApp.Models.DBObjects;

namespace JobsApp.Repository
{
    public class ApplicationRepository
    {
        private ApplicationDbContext dbContext;

        public ApplicationRepository() 
        {
            this.dbContext = new ApplicationDbContext();
        }

        public ApplicationRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<ApplicationModel> GetAllApplications()
        {
            List <ApplicationModel> applicationList = new List<ApplicationModel>();

            foreach(Application dbApplication in this.dbContext.Applications)
            {
                applicationList.Add(MapDbObjectToModel(dbApplication));
            }

            return applicationList;
        }

        // My Applications - for candidates
        public List<ApplicationModel> GetAllApplicationsByCandidateProfile(string idProfile)
        {
            List<ApplicationModel> applicationList = new List<ApplicationModel>();

            foreach (Application dbApplication in this.dbContext.Applications)
            {
                var cv = this.dbContext.Cvs.FirstOrDefault(x => x.Idcv == dbApplication.Idcv);
                if (cv.Idprofile == idProfile)
                {
                    applicationList.Add(MapDbObjectToModel(dbApplication));
                }
            }
            return applicationList;
        }

        // Application to my Announcement - for company owner on announcement
        public List<ApplicationModel> GetAllApplicationsByAnnouncement(Guid idAnnouncement)
        {
            List<ApplicationModel> applicationList = new List<ApplicationModel>();

            foreach (Application dbApplication in this.dbContext.Applications)
            {
                if(dbApplication.Idannouncement == idAnnouncement)
                {
                    applicationList.Add(MapDbObjectToModel(dbApplication));
                }
            }
            return applicationList;
        }

        public ApplicationModel GetApplicationById(Guid id)
        {
            return MapDbObjectToModel(dbContext.Applications.FirstOrDefault(x => x.Idapplication== id));
        }

        public void InsertApplication(ApplicationModel applicationModel, Guid cv, Guid announcement)
        {
            applicationModel.Idapplication = Guid.NewGuid();
            applicationModel.Idcv = cv;
            applicationModel.Idannouncement = announcement;
            applicationModel.Date = DateTime.Now;
            this.dbContext.Applications.Add(MapModelToDbObject(applicationModel));
            dbContext.SaveChanges();
        }

        public void DeleteApplication(ApplicationModel applicationModel)
        {
            Application existingApplication = this.dbContext.Applications.FirstOrDefault(x => x.Idapplication == applicationModel.Idapplication);
            if (existingApplication != null)
            {
                dbContext.Applications.Remove(existingApplication);
                dbContext.SaveChanges();
            }
        }

        private ApplicationModel MapDbObjectToModel(Application dbApplication)
        {
            ApplicationModel  applicationModel = new ApplicationModel();

            if(dbApplication != null) 
            {
                applicationModel.Idapplication = dbApplication.Idapplication;
                applicationModel.Idcv = dbApplication.Idcv;
                applicationModel.Idannouncement = dbApplication.Idannouncement;
                applicationModel.Date = dbApplication.Date;
            }

            return applicationModel;
        }

        private Application MapModelToDbObject(ApplicationModel applicationModel)
        {
            Application application = new Application();

            if(applicationModel != null)
            {
                application.Idapplication = applicationModel.Idapplication;
                application.Idcv = applicationModel.Idcv;
                application.Idannouncement = applicationModel.Idannouncement;
                application.Date = applicationModel.Date;
            }

            return application;
        }

    }
}
