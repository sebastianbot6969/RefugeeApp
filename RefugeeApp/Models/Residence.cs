namespace RefugeeApp.Models
{
    public class Residence
    {
        public int Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public List<Refugee> Residents { get; set; } = new List<Refugee>();
    }
}