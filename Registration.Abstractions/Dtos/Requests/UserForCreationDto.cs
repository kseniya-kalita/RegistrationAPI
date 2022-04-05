using System.ComponentModel.DataAnnotations;

namespace Registration.Abstractions.Dtos
{
    public class UserForCreationDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public CompanyForCreationDto Company { get; set; }
    }
}
