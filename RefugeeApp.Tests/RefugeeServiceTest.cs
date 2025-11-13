using Xunit;
using Moq;
using RefugeeApp.Models;
using RefugeeApp.Repositories;
using RefugeeApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefugeeApp.Tests
{
    public class RefugeeServiceTests
    {
        [Fact]
        public async Task GetAllAsync_ReturnsAllRefugees()
        {
            // Arrange
            var refugees = new List<Refugee>
            {
                new Refugee { Id = 1, FirstName = "Ahmed", LastName = "Ali" },
                new Refugee { Id = 2, FirstName = "Fatima", LastName = "Ali" }
            };

            var repoMock = new Mock<IRefugeeRepository>();
            repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(refugees);

            var service = new RefugeeService(repoMock.Object);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.Equal(2, ((List<Refugee>)result).Count);
            Assert.Contains(result, r => r.FirstName == "Ahmed");
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectRefugee()
        {
            // Arrange
            var refugee = new Refugee { Id = 1, FirstName = "Fatima" };
            var repoMock = new Mock<IRefugeeRepository>();
            repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(refugee);

            var service = new RefugeeService(repoMock.Object);

            // Act
            var result = await service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Fatima", result!.FirstName);
        }
    }
}
