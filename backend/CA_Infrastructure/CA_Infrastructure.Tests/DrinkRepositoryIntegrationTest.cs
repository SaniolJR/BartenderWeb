using Xunit;
using CA_Domain.Entities;
using CA_Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using CA_Infrastructure.Repositories;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

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

            var drink = new Drink
            {
                Name = "Test Drink",
                Recipe = "Vodka and Soda blyat",
                Ingredients = new List<Ingredient>{
                            new Ingredient {Name = "Vodka"},
                            new Ingredient {Name = "Soda"}}
            };

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

            var drink = new Drink
            {
                Id = 123,
                Name = "test Drink",
                Recipe = "drink it to test it",
                Ingredients = new List<Ingredient>{
                                    new Ingredient {Name = "Vodka"},
                                    new Ingredient {Name = "Polish Vodka"}}
            };
            context.Drinks.Add(drink);
            await context.SaveChangesAsync();

            // Act
            var output = await repository.GetDrinkByIdAsync(123);

            // Assert
            Assert.NotNull(output);
            Assert.Equal("test Drink", output.Name);
            Assert.Single(context.Drinks);
        }

        [Fact]
        public async Task GetIngredientByNameAsync_ExistingDrink_PerfectFittingName_ReturnDrink()
        {
            //Arrage
            var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb3")
            .Options;
            using var context = new MainDbContext(options);
            var repository = new DrinkRepository(context);

            var ingredient = new Ingredient { Id = 1, Name = "Cola" };
            context.Ingredients.Add(ingredient);
            await context.SaveChangesAsync();

            //Act
            var output = await repository.GetIngredientByNameAsync("Cola");

            //Assert
            Assert.NotNull(output);
            Assert.Equal(ingredient.Name, output.Name);
            Assert.Single(context.Ingredients);
        }

        [Fact]
        public async Task GetIngredientByNameAsync_NonExistingDrink_ReturnFalse()
        {
            //Arrage
            var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb5")
            .Options;
            using var context = new MainDbContext(options);
            var repository = new DrinkRepository(context);

            var ingredient = new Ingredient { Id = 1, Name = "Cola" };
            context.Ingredients.Add(ingredient);
            await context.SaveChangesAsync();

            //Act
            var output = await repository.GetIngredientByNameAsync("Pepsi");

            //Assert
            Assert.Null(output);
            Assert.Single(context.Ingredients);
        }

        [Fact]
        public async Task GetDrinksAsync_FiltersByVerifiedAndTextFilter_ReturnsCorrectDrinks()
        {
            var options = new DbContextOptionsBuilder<MainDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDbGetDrinksAsync1")
                .Options;
            using var context = new MainDbContext(options);
            var repo = new DrinkRepository(context);

            var drink1 = new Drink { Name = "Mojito", Recipe = "Rum, Mint", Verified = true };
            var drink2 = new Drink { Name = "Cola Drink", Recipe = "Cola", Verified = false };
            context.Drinks.AddRange(drink1, drink2);
            await context.SaveChangesAsync();

            var result = await repo.GetDrinksAsync(true, "Mojito", 0, new List<string>(), 0, 20);
            Assert.Single(result);
            Assert.Equal("Mojito", result[0].Name);
            Assert.True(result[0].Verified);
        }

        [Fact]
        public async Task GetDrinksAsync_FiltersByIngredients_ReturnsCorrectDrinks()
        {
            var options = new DbContextOptionsBuilder<MainDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDbGetDrinksAsync2")
                .Options;
            using var context = new MainDbContext(options);
            var repo = new DrinkRepository(context);

            var ingredient = new Ingredient { Name = "Rum" };
            var drink = new Drink { Name = "Mojito", Recipe = "Rum, Mint", Verified = true, Ingredients = new List<Ingredient> { ingredient } };
            ingredient.Drinks.Add(drink);
            context.Ingredients.Add(ingredient);
            context.Drinks.Add(drink);
            await context.SaveChangesAsync();

            var result = await repo.GetDrinksAsync(true, "", 0, new List<string> { "Rum" }, 0, 20);
            Assert.Single(result);
            Assert.Equal("Mojito", result[0].Name);
        }

        [Fact]
        public async Task GetDrinksAsync_FiltersByMissingIngredients_ReturnsCorrectDrinks()
        {
            var options = new DbContextOptionsBuilder<MainDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDbGetDrinksAsync3")
                .Options;
            using var context = new MainDbContext(options);
            var repo = new DrinkRepository(context);

            var ingredient1 = new Ingredient { Name = "Rum" };
            var ingredient2 = new Ingredient { Name = "Mint" };
            var drink = new Drink { Name = "Mojito", Recipe = "Rum, Mint", Verified = true, Ingredients = new List<Ingredient> { ingredient1, ingredient2 } };
            ingredient1.Drinks.Add(drink);
            ingredient2.Drinks.Add(drink);
            context.Ingredients.AddRange(ingredient1, ingredient2);
            context.Drinks.Add(drink);
            await context.SaveChangesAsync();

            var result = await repo.GetDrinksAsync(true, "", 1, new List<string> { "Rum" }, 0, 20);
            Assert.Single(result);
            Assert.Equal("Mojito", result[0].Name);
        }

        [Fact]
        public async Task GetDrinkByIdAsync_LoadsIngredients()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<MainDbContext>()
                .UseInMemoryDatabase(databaseName: "DrinkDbTest_LoadsIngredients")
                .Options;

            using var dbContext = new MainDbContext(options);

            var ingredient1 = new Ingredient { Name = "Rum" };
            var ingredient2 = new Ingredient { Name = "Mint" };
            var drink = new Drink
            {
                Name = "Mojito",
                Recipe = "Mix everything",
                Ingredients = new List<Ingredient> { ingredient1, ingredient2 }
            };
            dbContext.Drinks.Add(drink);
            await dbContext.SaveChangesAsync();

            var repository = new DrinkRepository(dbContext);

            // Act
            var result = await repository.GetDrinkByIdAsync(drink.Id);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Ingredients);
            Assert.Equal(2, result.Ingredients.Count);
            Assert.Contains(result.Ingredients, i => i.Name == "Rum");
            Assert.Contains(result.Ingredients, i => i.Name == "Mint");
        }
    }
}