using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration.EnvironmentVariables;


namespace CA_Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
    {
        public MainDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "CA_Presentation")))
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables();

            var config = builder.Build();
            var conn = config.GetConnectionString("DefaultConnection")
                       ?? "Server=(localdb)\\mssqllocaldb;Database=ProjLocalDb;Trusted_Connection=True;";

            var options = new DbContextOptionsBuilder<MainDbContext>();
            options.UseSqlServer(conn);
            return new MainDbContext(options.Options);
        }
    }
}