using ECommerce.Application.Abstractions;
using ECommerce.Domain.Abstractions;
using ECommerce.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace ECommerce.Infrastructure.DependencyInjections
{
    public static class DependencyInjection
    {
        #region Public Methods

        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            //services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            //services.AddScoped<IEmailService, EmailService>();
            //services.AddScoped<IEnvironmentService, EnvironmentService>();

            AddPersistence(services, configuration);

            return services;
        }

        #endregion Public Methods

        #region Private Methods

        private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(configuration), "Database connection string is missing!");
            }
            // Configure EF Core with SQL Server
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString).UseLazyLoadingProxies());

            services.AddScoped<ITokenService, TokenService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IExportService, ExportService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IActivityLogService, ActivityLogService>();
            services.AddScoped<IDbService>(sp => sp.GetRequiredService<AppDbContext>());
            // Call the method to add repository services
            services.AddRepositories();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.MSSqlServer(
                    connectionString: connectionString,
                    sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true }
                )
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .CreateLogger();
        }

        #endregion Private Methods
    }
}