using Xunit;
using CA_Domain.Entities;
using CA_Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using CA_Infrastructure.Repositories;

namespace CA_Infrastructure.Tests;

public class UserRepositoryIntegrationTests
{
    [Fact]
    public async Task GetByNickAsync_ExistingNick_ReturnsUser()
    {
        //arrange
        var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        using var context = new MainDbContext(options);

        //act
        context.Users.Add(new User
        {
            Username = "Emil G",
            Email = "test@test.com",
            Password = "hash",
            Role = "user"
        });
        await context.SaveChangesAsync();

        var repo = new UserRepository(context);
        var result = await repo.GetByNickAsync("Emil G");

        //assert
        Assert.NotNull(result);
        Assert.Equal("Emil G", result.Username);
    }

    [Fact]
    public async Task GetByNickAsync_WronkUserNick_ReturnsFalse()
    {
        //arrange
        var options = new DbContextOptionsBuilder<MainDbContext>()
           .UseInMemoryDatabase(Guid.NewGuid().ToString())
           .Options;
        using var context = new MainDbContext(options);

        //act
        context.Users.Add(new User
        {
            Username = "Emil G",
            Email = "test@test.com",
            Password = "hash",
            Role = "user"
        });
        await context.SaveChangesAsync();

        var repo = new UserRepository(context);
        var result = await repo.GetByNickAsync("Emil G");

        //assert
        Assert.NotNull(result);
        Assert.NotEqual("Emil H", result.Username);
    }
}