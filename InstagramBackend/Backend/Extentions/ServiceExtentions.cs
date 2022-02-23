using Backend.Data;
using Backend.Models;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Backend.Extentions
{
    public static class ServiceExtentions
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Backend", Version = "v1" });
            });
        }

        public static void ConfigureLoggerService(this IServiceCollection services) =>
         services.AddScoped<ILoggerManager, LoggerManager>();


        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
                 services.AddDbContext<AppDbContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<AppDbContext>();
        }

    }
}
