using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ECommerce.Application
{
    public static class DependencyInjection
    {
        #region Public Methods

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblies(
                    Assembly.GetExecutingAssembly() // Ensure it scans the entire application layer
                );
            });

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

            return services;
        }

        #endregion Public Methods
    }
}