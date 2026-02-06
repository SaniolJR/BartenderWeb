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
        var repo = new IngredientRepository(dbContext);

        // Act
        var result = await repo.GetIngredientByIdAsync(ingredient.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Ice", result.Name);
    }

    [Fact]
    public async Task GetIngredientsAsync_ReturnsAllIngredients()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var dbContext = new MainDbContext(options);

        var i1 = new Ingredient { Name = "Ice" };
        var i2 = new Ingredient { Name = "Whisky" };
        var i3 = new Ingredient { Name = "Cola" };

        dbContext.Ingredients.Add(i1);
        dbContext.Ingredients.Add(i2);
        dbContext.Ingredients.Add(i3);
        await dbContext.SaveChangesAsync();

        var repo = new IngredientRepository(dbContext);

        // Act
        var result = await repo.GetIngredientsAsync("", 0, 20);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
    }
    [Fact]
    public async Task GetIngredientsAsync_ReturnsFilteredIngredients()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var dbContext = new MainDbContext(options);

        var i1 = new Ingredient { Name = "Ice" };
        var i2 = new Ingredient { Name = "Whisky" };
        var i3 = new Ingredient { Name = "Cola" };

        dbContext.Ingredients.Add(i1);
        dbContext.Ingredients.Add(i2);
        dbContext.Ingredients.Add(i3);
        await dbContext.SaveChangesAsync();

        var repo = new IngredientRepository(dbContext);

        // Act
        var result = await repo.GetIngredientsAsync("i", 0, 20);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }
    [Fact]
    public async Task GetIngredientsAsync_ReturnsNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var dbContext = new MainDbContext(options);

        var i1 = new Ingredient { Name = "Ice" };
        var i2 = new Ingredient { Name = "Whisky" };
        var i3 = new Ingredient { Name = "Cola" };

        dbContext.Ingredients.Add(i1);
        dbContext.Ingredients.Add(i2);
        dbContext.Ingredients.Add(i3);
        await dbContext.SaveChangesAsync();

        var repo = new IngredientRepository(dbContext);

        // Act
        var result = await repo.GetIngredientsAsync("Vodka", 0, 20);

        // Assert
        Assert.Empty(result);
    }

}
