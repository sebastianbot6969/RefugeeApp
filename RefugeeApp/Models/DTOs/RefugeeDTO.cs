namespace RefugeeApp.Models.DTOs
{
    public class RefugeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }

        // Kun referencer – ingen navigation properties
        public string? ResidenceName { get; set; }
        public string? FamilyName { get; set; }
    }
}
