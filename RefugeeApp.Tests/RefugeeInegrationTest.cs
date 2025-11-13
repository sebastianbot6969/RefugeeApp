using Xunit;
using Microsoft.EntityFrameworkCore;
using RefugeeApp.Data;
using RefugeeApp.Models;
using RefugeeApp.Repositories;
using RefugeeApp.Services;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RefugeeApp.Tests
{
    public class RefugeeIntegrationTests
    {
        private RefugeeContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<RefugeeContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new RefugeeContext(options);

            // Seed testdata
            var family = new Family { FamilyName = "Ali" };
            var residence = new Residence { Name = "Test Home", City = "Test City", Address = "Main Street 1" };
            var refugees = new List<Refugee>
            {
                new Refugee { FirstName = "Ahmed", LastName = "Ali", Family = family, Residence = residence },
                new Refugee { FirstName = "Fatima", LastName = "Ali", Family = family, Residence = residence }
            };

            context.Families.Add(family);
            context.Residences.Add(residence);
            context.Refugees.AddRange(refugees);
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task RefugeeRepository_ReturnsAllRefugees()
        {
            // Arrange
            var context = GetInMemoryContext();
            var repo = new RefugeeRepository(context);

            // Act
            var result = await repo.GetAllAsync();

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(2, ((List<Refugee>)result).Count);
        }

        [Fact]
        public async Task RefugeeService_GetById_ReturnsCorrectRefugee()
        {
            // Arrange
            var context = GetInMemoryContext();
            var repo = new RefugeeRepository(context);
            var service = new RefugeeService(repo);

            // Act
            var refugee = await service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(refugee);
            Assert.Equal("Ahmed", refugee!.FirstName);
        }
    }
}
