using CA_Domain.Entities;
using CA_Infrastructure.Database;
using Microsoft.AspNetCore.Identity;

namespace CA_Infrastructure.DataSeeders
{
    public class UserSeeder : IUserSeeder
    {
        private readonly MainDbContext dbContext;

        public UserSeeder(MainDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Users.Any())
                {
                    var users = GetUsers();
                    dbContext.Users.AddRange(users);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<User> GetUsers()
        {
            var hasher = new PasswordHasher<User>();
            var user = new User
            {
                Nick = "Emil G",
                Email = "emilGitarzysta@gmail.com",
                Role = "user"
            };
            user.Passwd = hasher.HashPassword(user, "Haslo.123");
            return [user];
        }
    }
}