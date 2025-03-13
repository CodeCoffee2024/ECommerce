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
            return services;
        }

        #endregion Public Methods
    }
}