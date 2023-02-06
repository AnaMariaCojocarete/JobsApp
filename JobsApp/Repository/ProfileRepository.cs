using JobsApp.Data;
using JobsApp.Models;
using JobsApp.Models.DBObjects;

namespace JobsApp.Repository
{
    public class ProfileRepository
    {
        private ApplicationDbContext dbContext;

        public ProfileRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }

        public ProfileRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<ProfileModel> GetAllProfiles()
        {
            List<ProfileModel> profilesList = new List<ProfileModel>();

            foreach (Profile dbProfile in this.dbContext.Profiles)
            {
                profilesList.Add(MapDbObjectToModel(dbProfile));
            }
            return profilesList;
        }

        public ProfileModel GetProfileById(string id)
        {
            return MapDbObjectToModel(dbContext.Profiles.FirstOrDefault(x => x.Idprofile == id));
        }

        public void InsertProfile(ProfileModel profileModel)
        {
            dbContext.Profiles.Add(MapModelToDbObject(profileModel));
            dbContext.SaveChanges();
        }

        public void UpdateProfile(ProfileModel profileModel)
        {
            Profile existingProfile = dbContext.Profiles.FirstOrDefault(x => x.Idprofile == profileModel.Idprofile);
            if (existingProfile != null)
            {
                existingProfile.Idprofile = profileModel.Idprofile;
                existingProfile.Name = profileModel.Name;
                existingProfile.Description = profileModel.Description;
                existingProfile.Contact = profileModel.Contact;
                dbContext.SaveChanges();
            }
        }

        public void DeleteProfile(ProfileModel profileModel)
        {
            Profile existingProfile = dbContext.Profiles.FirstOrDefault(x => x.Idprofile == profileModel.Idprofile);

            if (existingProfile != null)
            {
                dbContext.Profiles.Remove(existingProfile);
                dbContext.SaveChanges();
            }
        }

        private ProfileModel MapDbObjectToModel(Profile dbProfile)
        {
            ProfileModel profileModel = new ProfileModel();

            if (dbProfile != null)
            {
                profileModel.Idprofile = dbProfile.Idprofile;
                profileModel.Name = dbProfile.Name;
                profileModel.Description = dbProfile.Description;
                profileModel.Contact = dbProfile.Contact;
            }

            return profileModel;
        }

        private Profile MapModelToDbObject(ProfileModel profileModel)
        {
            Profile profile = new Profile();

            if (profileModel != null)
            {
                profile.Idprofile = profileModel.Idprofile;
                profile.Name = profileModel.Name;
                profile.Description = profileModel.Description;
                profile.Contact = profileModel.Contact;
            }

            return profile;
        }
    }
}
