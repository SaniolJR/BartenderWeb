using Microsoft.EntityFrameworkCore;
using CA_Domain_Entities;

//class which represents whole DB instance in project
internal class MainDbContext : DbContext
{
    internal DBSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("TODO: add url to SQL server");
    }

}