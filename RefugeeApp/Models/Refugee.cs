namespace RefugeeApp.Models
{
    public class Refugee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int ResidenceId { get; set; }
        public Residence? Residence { get; set; }

        public int? FamilyId { get; set; }
        public Family? Family { get; set; }
    }
}