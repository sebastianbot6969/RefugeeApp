using RefugeeApp.Models;
using RefugeeApp.Data;

namespace RefugeeApp.Data
{
    public static class DbInitializer
    {
        public static void Seed(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<RefugeeContext>();

            // Sikrer at databasen findes
            context.Database.EnsureCreated();

            // Hvis der allerede findes data, spring over
            if (context.Refugees.Any())
                return;

            var residence1 = new Residence { Name = "Copenhagen Refugee Center", Address = "Østerbrogade 100", City = "Copenhagen" };
            var residence2 = new Residence { Name = "Aarhus Refugee Home", Address = "Søndergade 45", City = "Aarhus"};

            var family1 = new Family { FamilyName = "Ali" };
            var family2 = new Family { FamilyName = "Hassan" };

            var refugees = new List<Refugee>
            {
                new Refugee { FirstName = "Ahmed", LastName = "Ali", DateOfBirth = new DateTime(1990,5,1), Residence = residence1, Family = family1 },
                new Refugee { FirstName = "Fatima", LastName = "Ali", DateOfBirth = new DateTime(1992,3,15), Residence = residence1, Family = family1 },
                new Refugee { FirstName = "Yusuf", LastName = "Ali", DateOfBirth = new DateTime(2015,8,22), Residence = residence1, Family = family1 },
                new Refugee { FirstName = "Mariam", LastName = "Hassan", DateOfBirth = new DateTime(1988,11,9), Residence = residence2, Family = family2 },
                new Refugee { FirstName = "Omar", LastName = "Hassan", DateOfBirth = new DateTime(2013,1,2), Residence = residence2, Family = family2 }
            };

            context.AddRange(refugees);
            context.SaveChanges();
        }
    }
}
