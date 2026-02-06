using Microsoft.Extensions.DependencyInjection;
using WarehouseManagement.Application.Services;
using WarehouseManagement.Application.Services.IServices;

namespace WarehouseManagement.Application
{
    public static class DependenceInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IStoreService, StoreService>();

            return services;
        }
    }
}
