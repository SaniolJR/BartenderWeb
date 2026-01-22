using Xunit;
using Moq;
using System.Reflection.Metadata.Ecma335;
using CA_Domain.Entities;
using CA_Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using CA_Infrastructure.Repositories;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;


public class DrinkRepositoryUnitTest
{
    [Fact]
    public async Task AddDrinkAsync_ValidDrinkTestRun_True()
    {
        // Arrange
        var mockSet = new Mock<DbSet<Drink>>();
        var mockContext = new Mock<MainDbContext>(new DbContextOptions<MainDbContext>());
        mockContext.Setup(m => m.Drinks).Returns(mockSet.Object);

        var repository = new DrinkRepository(mockContext.Object);
        var drink = new Drink
        {
            Name = "Test Drink",
            Recipe = "test Recipe",
            Ingredients = new List<Ingredient>
            {
                new Ingredient { Name = "Rum" },
                new Ingredient { Name = "Cola" }
            }
        };

        // Act
        var result = await repository.AddDrinkAsync(drink);

        // Assert
        mockSet.Verify(m => m.Add(It.IsAny<Drink>()), Times.Once);
        mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal("Test Drink", result.Name);
    }

    [Fact]
    public async Task GetDrinkByIdAsync_ExistingIDTestRun_True()
    {
        //Arrange
        var mockSet = new Mock<DbSet<Drink>>();
        var mockContext = new Mock<MainDbContext>(new DbContextOptions<MainDbContext>());
        mockContext.Setup(m => m.Drinks).Returns(mockSet.Object);

        var repository = new DrinkRepository(mockContext.Object);
        var drink = new Drink
        {
            Name = "Test Drink",
            Recipe = "test Recipe",
            Ingredients = new List<Ingredient>
            {
                new Ingredient { Name = "Rum" },
                new Ingredient { Name = "Cola" }
            }
        };

        // Act
        var result = await repository.GetDrinkByIdAsync(123);

        // Assert
        mockSet.Verify(m => m.FindAsync(It.IsAny<object[]>()), Times.Once);
    }
}