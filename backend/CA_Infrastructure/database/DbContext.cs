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

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Drink> Drinks { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
    }
}