using WarehouseManagement.Api.Middlewares;
using WarehouseManagement.Api.Services;
using WarehouseManagement.Application.Interfaces;

namespace WarehouseManagement.Api
{
    public static class DependenceInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddScoped<StoreContextMiddleware>();

            services.AddScoped<IStoreIdResolver, RouteStoreIdResolver>();

            services.AddScoped<IStoreContext, StoreContext>();

            return services;
        }
    }
}
