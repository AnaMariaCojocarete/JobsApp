using System;
using System.Collections.Generic;

namespace JobsApp.Models.DBObjects
{
    public partial class Application
    {
        public Guid Idapplication { get; set; }
        public Guid Idannouncement { get; set; }
        public Guid Idcv { get; set; }
        public DateTime Date { get; set; }

        public virtual Announcement IdannouncementNavigation { get; set; } = null!;
        public virtual Cv IdcvNavigation { get; set; } = null!;
    }
}
