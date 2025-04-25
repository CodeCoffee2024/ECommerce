using ECommerce.Domain.Entities.Log.Interfaces;
using ECommerce.Domain.Entities.Settings.Interfaces;
using ECommerce.Domain.Entities.UserManagement.Interfaces;
using ECommerce.Infrastructure.Repositories.Log;
using ECommerce.Infrastructure.Repositories.Settings;
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
            services.AddScoped<IActivityLogRepository, ActivityLogRepository>();
            services.AddScoped<IUnitOfMeasurementConversionRepository, UnitOfMeasurementConversionRepository>();
            services.AddScoped<IUnitOfMeasurementTypeRepository, UnitOfMeasurementTypeRepository>();
            services.AddScoped<IUnitOfMeasurementRepository, UnitOfMeasurementRepository>();
            services.AddScoped<IUserUserPermissionRepository, UserUserPermissionRepository>();
            return services;
        }

        #endregion Public Methods
    }
}