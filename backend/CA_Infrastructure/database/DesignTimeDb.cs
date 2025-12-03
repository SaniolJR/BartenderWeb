using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration.EnvironmentVariables;


namespace CA_Infrastructure
{
    //class with context factory, needed in EF migrations
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
    {
        public MainDbContext CreateDbContext(string[] args)
        {
            //set path to appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "CA_Presentation")))
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            //set connection string - if appsetting dont have - use local
            var conn = config.GetConnectionString("DefaultConnection")
                       ?? "Server=(localdb)\\mssqllocaldb;Database=ProjLocalDb;Trusted_Connection=True;";

            //create db context
            var options = new DbContextOptionsBuilder<MainDbContext>();
            options.UseSqlServer(conn);
            return new MainDbContext(options.Options);
        }
    }
}