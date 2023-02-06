using System.ComponentModel.DataAnnotations;

namespace JobsApp.Models
{
    public class ProfileModel
    {
        public string Idprofile { get; set; }

        [StringLength(50, ErrorMessage = "String too long (max. 50 chars)")]
        public string Name { get; set; }
        public string Description { get; set; }

        [StringLength(1000, ErrorMessage = "String too long (max. 1000 chars)")]
        public string Contact { get; set; }
    }
}
