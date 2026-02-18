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

    [Fact]
    public async Task AddIngredientAsync_ValidIngredient_AddsIngredientToDatabase()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(databaseName: "IngredientTestDb1")
            .Options;
        using var context = new MainDbContext(options);
        var repository = new IngredientRepository(context);

        var ingredient = new Ingredient
        {
            Name = "Lime"
        };

        // Act
        var result = await repository.AddIngredientAsync(ingredient);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Lime", result.Name);
        Assert.Single(context.Ingredients);
    }

    [Fact]
    public async Task GetIngredientByIdAsync_ExistingId_ReturnsIngredient()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(databaseName: "IngredientTestDb2")
            .Options;
        using var context = new MainDbContext(options);
        var repository = new IngredientRepository(context);

        var ingredient = new Ingredient
        {
            Id = 123,
            Name = "Sugar"
        };
        context.Ingredients.Add(ingredient);
        await context.SaveChangesAsync();

        // Act
        var output = await repository.GetIngredientByIdAsync(123);

        // Assert
        Assert.NotNull(output);
        Assert.Equal("Sugar", output.Name);
        Assert.Single(context.Ingredients);
    }

    [Fact]
    public async Task GetIngredientByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(databaseName: "IngredientTestDb3")
            .Options;
        using var context = new MainDbContext(options);
        var repository = new IngredientRepository(context);

        var ingredient = new Ingredient
        {
            Id = 1,
            Name = "Salt"
        };
        context.Ingredients.Add(ingredient);
        await context.SaveChangesAsync();

        // Act
        var output = await repository.GetIngredientByIdAsync(999);

        // Assert
        Assert.Null(output);
        Assert.Single(context.Ingredients);
    }

    [Fact]
    public async Task AddIngredientAsync_MultipleIngredients_AddsAllToDatabase()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(databaseName: "IngredientTestDb4")
            .Options;
        using var context = new MainDbContext(options);
        var repository = new IngredientRepository(context);

        var ingredient1 = new Ingredient { Name = "Vodka" };
        var ingredient2 = new Ingredient { Name = "Orange Juice" };

        // Act
        await repository.AddIngredientAsync(ingredient1);
        await repository.AddIngredientAsync(ingredient2);

        // Assert
        Assert.Equal(2, context.Ingredients.Count());
        var allIngredients = await context.Ingredients.ToListAsync();
        Assert.Contains(allIngredients, i => i.Name == "Vodka");
        Assert.Contains(allIngredients, i => i.Name == "Orange Juice");
    }

    [Fact]
    public async Task GetIngredientByIdAsync_AfterAddingIngredient_ReturnsCorrectIngredient()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(databaseName: "IngredientTestDb5")
            .Options;
        using var context = new MainDbContext(options);
        var repository = new IngredientRepository(context);

        var ingredient = new Ingredient { Name = "Mint" };
        var addedIngredient = await repository.AddIngredientAsync(ingredient);

        // Act
        var output = await repository.GetIngredientByIdAsync(addedIngredient.Id);

        // Assert
        Assert.NotNull(output);
        Assert.Equal("Mint", output.Name);
        Assert.Equal(addedIngredient.Id, output.Id);
    }


}
