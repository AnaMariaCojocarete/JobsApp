using System;
using System.Collections.Generic;

namespace JobsApp.Models.DBObjects
{
    public partial class Announcement
    {
        public Announcement()
        {
            Applications = new HashSet<Application>();
        }

        public Guid Idannouncement { get; set; }
        public string Idprofile { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string ExperienceLevel { get; set; } = null!;

        public virtual Profile IdprofileNavigation { get; set; } = null!;
        public virtual ICollection<Application> Applications { get; set; }
    }
}
