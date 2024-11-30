using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DocumentDomain.Interfaces;
using DocumentInfrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace DocumentInfrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraStructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = configuration.GetSection("Auth:Authority").Value; 
                options.Audience = configuration.GetSection("Auth:Audience").Value;
            });
            
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IFormat, FormatRepository>();
            services.AddScoped<IXmlValidation, XmlValidationRepository>();
            services.AddScoped<IJsonBuild, JsonBuildRepository>();
            return services;
        }
    }
}
