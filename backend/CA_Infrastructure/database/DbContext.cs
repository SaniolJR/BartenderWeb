using Microsoft.EntityFrameworkCore;
using CA_Domain.Entities;

//class which represents whole DB instance in project

namespace CA_Infrastructure.Database
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            //in options is URL to DB passed by framework
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}