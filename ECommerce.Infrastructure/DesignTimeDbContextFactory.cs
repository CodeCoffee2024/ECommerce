using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        #region Public Methods

        public AppDbContext CreateDbContext(string[] args)
        {
            // Get the correct path to the API project's appsettings.json
            string basePath = Path.Combine(Directory.GetCurrentDirectory(), "../ECommerce.Api");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath) // Explicitly set base path to API project
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true) // Load Development settings if available
                .AddEnvironmentVariables()
                .Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string is missing!");

            Console.WriteLine($"[EF Migrations] Using Connection String: {connectionString}");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }

        #endregion Public Methods
    }
}