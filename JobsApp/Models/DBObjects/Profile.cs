using System;
using System.Collections.Generic;

namespace JobsApp.Models.DBObjects
{
    public partial class Profile
    {
        public Profile()
        {
            Announcements = new HashSet<Announcement>();
            Cvs = new HashSet<Cv>();
        }

        public string Idprofile { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Contact { get; set; } = null!;

        public virtual ICollection<Announcement> Announcements { get; set; }
        public virtual ICollection<Cv> Cvs { get; set; }
    }
}
