using System;
using System.Collections.Generic;

namespace JobsApp.Models.DBObjects
{
    public partial class Cv
    {
        public Cv()
        {
            Applications = new HashSet<Application>();
        }

        public Guid Idcv { get; set; }
        public string Idprofile { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? Address { get; set; }
        public string Mail { get; set; } = null!;
        public string? Phone { get; set; }
        public string? JobTitle { get; set; }
        public string? JobExperience { get; set; }
        public string? HardSkills { get; set; }
        public string? SoftSkills { get; set; }
        public string? Education { get; set; }
        public string? Certifications { get; set; }

        public virtual Profile IdprofileNavigation { get; set; } = null!;
        public virtual ICollection<Application> Applications { get; set; }
    }
}
