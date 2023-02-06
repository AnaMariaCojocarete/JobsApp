using JobsApp.Data;
using JobsApp.Models;
using JobsApp.Models.DBObjects;

namespace JobsApp.Repository
{
    public class AnnouncementRepository
    {
        private ApplicationDbContext dbContext;

        public AnnouncementRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }

        public AnnouncementRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<AnnouncementModel> GetAllAnnouncementsByProfile(string idProfile)
        {
            List<AnnouncementModel> announcementsList = new List<AnnouncementModel>();

            foreach (Announcement dbAnnouncement in this.dbContext.Announcements)
            {
                if(dbAnnouncement.Idprofile == idProfile)
                {
                    announcementsList.Add(MapDbObjectToModel(dbAnnouncement));
                }
            }
            return announcementsList;
        }

        public List<AnnouncementModel> GetAllAnnouncements()
        {
            List<AnnouncementModel> announcementsList = new List<AnnouncementModel>();

            foreach (Announcement dbAnnouncement in this.dbContext.Announcements)
            {
                announcementsList.Add(MapDbObjectToModel(dbAnnouncement));
            }
            return announcementsList;
        }

        public AnnouncementModel GetAnnouncementById(Guid id)
        {
            return MapDbObjectToModel(dbContext.Announcements.FirstOrDefault(x => x.Idannouncement == id));
        }

        public void InsertAnnouncement(AnnouncementModel announcementModel, string idProfile)
        {
            announcementModel.Idannouncement = Guid.NewGuid();
            announcementModel.Idprofile = idProfile;
            dbContext.Announcements.Add(MapModelToDbObject(announcementModel));
            dbContext.SaveChanges();
        }

        public void UpdateAnnouncement(AnnouncementModel announcementModel)
        {
            Announcement existingAnnouncement = dbContext.Announcements.FirstOrDefault(x => x.Idannouncement== announcementModel.Idannouncement);
            if (existingAnnouncement != null)
            {
                existingAnnouncement.Idannouncement = announcementModel.Idannouncement;
                existingAnnouncement.Idprofile = announcementModel.Idprofile;
                existingAnnouncement.Title = announcementModel.Title;
                existingAnnouncement.Description = announcementModel.Description;
                existingAnnouncement.Location = announcementModel.Location;
                existingAnnouncement.ExperienceLevel = announcementModel.ExperienceLevel;
                dbContext.SaveChanges();
            }
        }

        public void DeleteAnnouncement(AnnouncementModel announcementModel)
        {
            Announcement existingAnnouncement = dbContext.Announcements.FirstOrDefault(x => x.Idannouncement == announcementModel.Idannouncement);
            if (existingAnnouncement != null)
            {
                dbContext.Announcements.Remove(existingAnnouncement);
                dbContext.SaveChanges();
            }
        }

        private AnnouncementModel MapDbObjectToModel(Announcement dbAnnouncement)
        {
            AnnouncementModel announcementModel = new AnnouncementModel();

            if(dbAnnouncement != null)
            {
                announcementModel.Idannouncement = dbAnnouncement.Idannouncement;
                announcementModel.Idprofile = dbAnnouncement.Idprofile;
                announcementModel.Title = dbAnnouncement.Title;
                announcementModel.Description = dbAnnouncement.Description;
                announcementModel.Location = dbAnnouncement.Location;
                announcementModel.ExperienceLevel = dbAnnouncement.ExperienceLevel;
            }
            return announcementModel;
        }

        private Announcement MapModelToDbObject(AnnouncementModel announcementModel)
        {
            Announcement announcement = new Announcement();

            if(announcementModel != null)
            {
                announcement.Idannouncement = announcementModel.Idannouncement;
                announcement.Idprofile = announcementModel.Idprofile;
                announcement.Title = announcementModel.Title;
                announcement.Description = announcementModel.Description;
                announcement.Location = announcementModel.Location;
                announcement.ExperienceLevel = announcementModel.ExperienceLevel;
            }

            return announcement;
        }
    }
}
