using System.ComponentModel.DataAnnotations;

namespace RefugeeApp.Models.DTOs
{
    public class CreateRefugeeDto
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public int ResidenceId { get; set; }

        // FamilyId kan være null, hvis personen ikke tilhører en familie endnu
        public int? FamilyId { get; set; }
    }
}
