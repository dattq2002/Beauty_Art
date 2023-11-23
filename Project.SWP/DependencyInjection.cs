using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Project.SWP.Services;
using Services;
using Services.Service;
using System;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Project.SWP
{
    public static class DependencyInjection
    {
        public static IServiceCollection BuildServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region AppSettings
            services.AddHttpContextAccessor();
            services.AddScoped<IClaimsServices, ClaimsServices>();
            var connectionString = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
            services.AddSingleton(connectionString);
            services.AddDbContext<AppDBContext>(option => option.UseSqlServer(connectionString.SQLServerDB));
            #endregion
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers().AddJsonOptions(opt =>
            opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            #region SwaggerConfig
            services.AddSwaggerGen(
                    c =>
                    {
                        c.SwaggerDoc("v1", new OpenApiInfo
                        {
                            Title = "SWP_Project",
                            Version = "v1",
                            Description = "This is Our API",
                            Contact = new OpenApiContact
                            {
                                Url = new Uri("https://google.com")
                            }
                        });
                        var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                        c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
                    });
            #endregion
            return services;
        }
    }
}
