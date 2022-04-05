using System.ComponentModel.DataAnnotations;

namespace Registration.Abstractions.Dtos
{
    public class CompanyForCreationDto
    {
        [Required]
        public string Name { get; set; }
    }
}
