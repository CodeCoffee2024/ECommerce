using ECommerce.Application.Abstractions;
using ECommerce.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            string connectionString = configuration.GetConnectionString("DefaultConnection") ??
                                      throw new ArgumentNullException(nameof(configuration));

            // Configure EF Core with PostgreSQL
            services.AddDbContext<AppDbContext>(options =>
                 options.UseSqlServer(connectionString).UseLazyLoadingProxies());

            services.AddScoped<ITokenService, TokenService>();
            services.AddSingleton<IPermissionService, PermissionService>();
            // Call the method to add repository services
            services.AddRepositories();
        }

        #endregion Private Methods
    }
}