using Xunit;
using CA_Domain.Entities;
using CA_Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using CA_Infrastructure.Repositories;

namespace CA_Infrastructure.Tests;

public class RefreshTokenRepositoryIntegrationTests
{
    [Fact]
    public async Task GetByTokenAsync_TokenExists_ReturnTrue()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        using var context = new MainDbContext(options);

        var user = new User { Username = "Emil G", Email = "test@test.com", Passwd = "hash", Role = "user" };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var token = new RefreshToken
        {
            Token = "testtoken",
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };
        context.RefreshTokens.Add(token);
        await context.SaveChangesAsync();

        var repo = new RefreshTokenRepository(context);

        // Act
        var result = await repo.GetByTokenAsync("testtoken");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("testtoken", result.Token);
        Assert.NotNull(result.UserObj);
    }

    [Fact]
    public async Task GetByTokenAsync_TokenDoesNotExist_ReturnFalse()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        using var context = new MainDbContext(options);
        var repo = new RefreshTokenRepository(context);

        // Act
        var result = await repo.GetByTokenAsync("nieistniejacy");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_AddsTokenToDatabase()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        using var context = new MainDbContext(options);
        var repo = new RefreshTokenRepository(context);

        var token = new RefreshToken
        {
            Token = "testtoken",
            UserId = 1,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        // Act
        await repo.AddAsync(token);

        // Assert
        Assert.Single(context.RefreshTokens);
        Assert.Equal("testtoken", context.RefreshTokens.First().Token);
    }

    [Fact]
    public async Task RevokeAsync_SetsIsRevokedTrue()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        using var context = new MainDbContext(options);

        var token = new RefreshToken
        {
            Token = "testtoken",
            UserId = 1,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };
        context.RefreshTokens.Add(token);
        await context.SaveChangesAsync();

        var repo = new RefreshTokenRepository(context);

        // Act
        await repo.RevokeAsync(token);

        // Assert
        Assert.True(token.IsRevoked);
        var fromDb = await context.RefreshTokens.FindAsync(token.Id);
        Assert.True(fromDb!.IsRevoked);
    }
}