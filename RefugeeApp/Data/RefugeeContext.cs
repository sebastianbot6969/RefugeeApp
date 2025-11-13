    using Microsoft.EntityFrameworkCore;
    using RefugeeApp.Models;

    namespace RefugeeApp.Data
    {
        public class RefugeeContext : DbContext
        {
            public RefugeeContext(DbContextOptions<RefugeeContext> options)
                : base(options)
            {
            }

            public DbSet<Refugee> Refugees { get; set; }
            public DbSet<Family> Families { get; set; }
            public DbSet<Residence> Residences { get; set; }
        }
    }
