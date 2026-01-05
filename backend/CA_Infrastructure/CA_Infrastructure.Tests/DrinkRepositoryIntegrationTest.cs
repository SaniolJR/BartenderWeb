using Xunit;
using CA_Domain.Entities;
using CA_Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using CA_Infrastructure.Repositories;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace CA_Infrastructure.Tests
{
    public class DrinkRepositoryIntegrationTests
    {
        [Fact]
        public async Task AddDrinkAsync_ValidDrink_AddsDrinkToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MainDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb1")
                .Options;
            using var context = new MainDbContext(options);
            var repository = new DrinkRepository(context);

            var drink = new Drink { Name = "Test Drink", Receipe = "Vodka and Soda blyat" };

            // Act
            var result = await repository.AddDrinkAsync(drink);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Drink", result.Name);
            Assert.Single(context.Drinks);
        }

        [Fact]
        public async Task GetDrinkByIdAsync_ExistingId_ReturnsDrink()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb2")
            .Options;
            using var context = new MainDbContext(options);
            var repository = new DrinkRepository(context);

            var drink = new Drink { Id = 123, Name = "test Drink", Receipe = "drink it to test it" };
            context.Drinks.Add(drink);
            await context.SaveChangesAsync();

            // Act
            var output = await repository.GetDrinkByIdAsync(123);

            // Assert
            Assert.NotNull(output);
            Assert.Equal("test Drink", output.Name);
            Assert.Single(context.Drinks);
        }
    }
}