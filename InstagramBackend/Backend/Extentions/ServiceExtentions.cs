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
    }
}
