using JobsApp.Data;
using JobsApp.Models;
using JobsApp.Models.DBObjects;

namespace JobsApp.Repository
{
    public class CVRepository
    {
        private ApplicationDbContext dbContext;

        public CVRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }

        public CVRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<CVModel> GetAllCVsByProfile(string idPRofile)
        {
            List<CVModel> cvsList = new List<CVModel>();

            foreach (Cv dbCv in this.dbContext.Cvs)
            {
                if (dbCv.Idprofile == idPRofile)
                {
                    cvsList.Add(MapDbObjectToModel(dbCv));
                }
            }
            return cvsList;
        }

        public CVModel GetCVById(Guid id)
        {
            return MapDbObjectToModel(dbContext.Cvs.FirstOrDefault(x => x.Idcv == id));
        }

        public void InsertCV(CVModel cVModel, string idProfile)
        {
            cVModel.Idcv = Guid.NewGuid();
            cVModel.Idprofile = idProfile;
            dbContext.Cvs.Add(MapModelToDbObject(cVModel));
            dbContext.SaveChanges();
        }

        public void UpdateCV(CVModel cVModel)
        {
            Cv existingCV = dbContext.Cvs.FirstOrDefault(x => x.Idcv == cVModel.Idcv);
            if (existingCV != null)
            {
                existingCV.Idcv = cVModel.Idcv;
                existingCV.Idprofile = cVModel.Idprofile;
                existingCV.LastName = cVModel.LastName;
                existingCV.FirstName = cVModel.FirstName;
                existingCV.Address = cVModel.Address;
                existingCV.Mail = cVModel.Mail;
                existingCV.Phone = cVModel.Phone;
                existingCV.JobTitle = cVModel.JobTitle;
                existingCV.JobExperience = cVModel.JobExperience;
                existingCV.HardSkills = cVModel.HardSkills;
                existingCV.SoftSkills = cVModel.SoftSkills;
                existingCV.Education = cVModel.Education;
                existingCV.Certifications = cVModel.Certifications;
                dbContext.SaveChanges();
            }
        }

        public void DeleteCV(CVModel cVModel)
        {
            Cv existingCV = dbContext.Cvs.FirstOrDefault(x => x.Idcv == cVModel.Idcv);
            if (existingCV != null)
            {
                dbContext.Cvs.Remove(existingCV);
                dbContext.SaveChanges();
            }
        }

        private CVModel MapDbObjectToModel(Cv dbCV)
        {
            CVModel cvModel = new CVModel();

            if(dbCV != null)
            {
                cvModel.Idcv = dbCV.Idcv;
                cvModel.Idprofile = dbCV.Idprofile;
                cvModel.LastName = dbCV.LastName;
                cvModel.FirstName = dbCV.FirstName;
                cvModel.Address = dbCV.Address;
                cvModel.Mail = dbCV.Mail;
                cvModel.Phone = dbCV.Phone;
                cvModel.JobTitle = dbCV.JobTitle;
                cvModel.JobExperience = dbCV.JobExperience;
                cvModel.HardSkills = dbCV.HardSkills;
                cvModel.SoftSkills = dbCV.SoftSkills;
                cvModel.Education = dbCV.Education;
                cvModel.Certifications = dbCV.Certifications;
            }

            return cvModel;
        }

        private Cv MapModelToDbObject(CVModel cvModel)
        {
            Cv cv = new Cv();

            if(cvModel != null)
            {
                cv.Idcv = cvModel.Idcv;
                cv.Idprofile = cvModel.Idprofile;
                cv.LastName = cvModel.LastName;
                cv.FirstName = cvModel.FirstName;
                cv.Address = cvModel.Address;
                cv.Mail = cvModel.Mail;
                cv.Phone = cvModel.Phone;
                cv.JobTitle = cvModel.JobTitle;
                cv.JobExperience = cvModel.JobExperience;
                cv.HardSkills = cvModel.HardSkills;
                cv.SoftSkills = cvModel.SoftSkills;
                cv.Education = cvModel.Education;
                cv.Certifications = cvModel.Certifications;
            }

            return cv;
        }

    }
}
