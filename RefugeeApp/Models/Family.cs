namespace RefugeeApp.Models
{
    public class Family
    {
        public int Id { get; set; }
        public string FamilyName { get; set; } = string.Empty;

        public List<Refugee> Members { get; set; } = new List<Refugee>();
    }
}