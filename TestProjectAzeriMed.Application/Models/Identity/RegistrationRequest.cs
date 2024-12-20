using System.ComponentModel.DataAnnotations;

namespace TestProjectAzeriMed.Application.Models.Identity
{
    public class RegistrationRequest
    {
        [Required]
        [MinLength(6)]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
