using Xunit;
using Moq;
using CA_Domain.Entities;
using CA_Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using CA_Infrastructure.Repositories;

public class RefreshTokenRepositoryUnitTests
{
    [Fact]
    public async Task AddAsync_AddTokenProperly_ReturnTrue()
    {
        // Arrange
        var mockSet = new Mock<DbSet<RefreshToken>>();
        var mockContext = new Mock<MainDbContext>(new DbContextOptions<MainDbContext>());
        mockContext.Setup(m => m.RefreshTokens).Returns(mockSet.Object);

        var repository = new RefreshTokenRepository(mockContext.Object);
        var token = new RefreshToken
        {
            Token = "testtoken",
            UserId = 1,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        // Act
        await repository.AddAsync(token);

        // Assert
        mockSet.Verify(m => m.Add(It.IsAny<RefreshToken>()), Times.Once);
        mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task RevokeAsync_MakeTokenRevoked_ReturnTrue()
    {
        // Arrange
        var mockContext = new Mock<MainDbContext>(new DbContextOptions<MainDbContext>());
        var repository = new RefreshTokenRepository(mockContext.Object);
        var token = new RefreshToken
        {
            Token = "testtoken",
            UserId = 1,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        // Act
        await repository.RevokeAsync(token);

        // Assert
        Assert.True(token.IsRevoked);
        mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}