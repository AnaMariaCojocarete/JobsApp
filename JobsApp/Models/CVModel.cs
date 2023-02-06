namespace JobsApp.Models
{
    public class CVModel
    {
        public Guid Idcv { get; set; }
        public string Idprofile { get; set; }
        public string LastName { get; set; } 
        public string FirstName { get; set; } 
        public string? Address { get; set; }
        public string Mail { get; set; } 
        public string? Phone { get; set; }
        public string? JobTitle { get; set; }
        public string? JobExperience { get; set; }
        public string? HardSkills { get; set; }
        public string? SoftSkills { get; set; }
        public string? Education { get; set; }
        public string? Certifications { get; set; }
    }
}
