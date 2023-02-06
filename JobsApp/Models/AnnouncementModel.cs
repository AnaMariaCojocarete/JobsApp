namespace JobsApp.Models
{
    public class AnnouncementModel
    {
        public Guid Idannouncement { get; set; }
        public string Idprofile { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string ExperienceLevel { get; set; }
    }
}
