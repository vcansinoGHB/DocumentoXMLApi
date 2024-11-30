using DocumentApplication;
using DocumentInfrastructure;

namespace DocumentAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDI().AddInfraStructureDI(configuration);
            return services;
        }
    }
}
