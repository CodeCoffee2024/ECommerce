using ECommerce.Domain.Entities.UserManagement.Interfaces;
using ECommerce.Infrastructure.Repositories.UserManagement;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure.DependencyInjections
{
    public static class RepositoryInjection
    {
        #region Public Methods

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserPermissionRepository, UserPermissionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IUserUserPermissionRepository, UserUserPermissionRepository>();
            return services;
        }

        #endregion Public Methods
    }
}