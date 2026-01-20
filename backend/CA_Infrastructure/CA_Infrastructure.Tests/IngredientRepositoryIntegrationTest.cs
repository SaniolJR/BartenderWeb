using System.Threading.Tasks;
using CA_Domain.Entities;
using CA_Infrastructure.Database;
using CA_Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class IngredientRepositoryIntegrationTests
{
    private MainDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(databaseName: "IngredientDb")
            .Options;
        return new MainDbContext(options);
    }

    [Fact]
    public async Task AddIngredientAsync_PersistsIngredient()
    {
        // Arrange
        var dbContext = GetDbContext();
        var repo = new IngredientRepository(dbContext);
        var ingredient = new Ingredient { Name = "Mint" };

        // Act
        var result = await repo.AddIngredientAsync(ingredient);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Mint", result.Name);
        Assert.NotEqual(0, result.Id);
        var fromDb = await dbContext.Ingredients.FindAsync(result.Id);
        Assert.NotNull(fromDb);
    }

    [Fact]
    public async Task GetIngredientByIdAsync_ReturnsCorrectIngredient()
    {
        // Arrange
        var dbContext = GetDbContext();
        var ingredient = new Ingredient { Name = "Ice" };
        dbContext.Ingredients.Add(ingredient);
        await dbContext.SaveChangesAsync();
        var repo = new IngredientRepository(dbContext);

        // Act
        var result = await repo.GetIngredientByIdAsync(ingredient.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Ice", result.Name);
    }
}