using System.Text;
using System.Text.Json.Serialization;
using Beauty_Art.API.Constants;
using Beauty_Art.Domains.Models;
using Beauty_Art.Repository.Implement;
using Beauty_Art.Repository.Interfaces;
using Beauty_Art.Service.Implement;
using Beauty_Art.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Beauty_Art.API.Extensions;

public static class DependencyServices
{
	public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
	{
		services.AddScoped<IUnitOfWork<BEAUTIFUL_ARTSContext>, UnitOfWork<BEAUTIFUL_ARTSContext>>();
		return services;
	}

	public static IServiceCollection AddDatabase(this IServiceCollection services)
	{
		IConfiguration configuration = new ConfigurationBuilder().AddEnvironmentVariables(EnvironmentVariableConstant.Prefix).Build();
		services.AddDbContext<BEAUTIFUL_ARTSContext>(options => options.UseSqlServer(CreateConnectionString(configuration)));
		return services;
	}

	private static string CreateConnectionString(IConfiguration configuration)
	{
		string connectionString = $"Server={configuration.GetValue<string>(DatabaseConstant.Host)},{configuration.GetValue<string>(DatabaseConstant.Port)};User Id={configuration.GetValue<string>(DatabaseConstant.UserName)};Password={configuration.GetValue<string>(DatabaseConstant.Password)};Database={configuration.GetValue<string>(DatabaseConstant.Database)}";
		return connectionString;
	}

	public static IServiceCollection AddServices(this IServiceCollection services)
	{
        #region AddScrope
		services.AddScoped<IAccountService, AccountService>();
		services.AddScoped<ICourseService, CourseService>();
		services.AddScoped<IPostService, PostService>();
		services.AddScoped<IInstructorService, InstructorService>();
		services.AddScoped<IWalletTypeService, WalletTypeService>();
		services.AddScoped<IOrderService, OrderService>();

        #endregion
        return services;
	}

	//public static IServiceCollection AddJwtValidation(this IServiceCollection services)
	//{
	//	IConfiguration configuration = new ConfigurationBuilder().AddEnvironmentVariables(EnvironmentVariableConstant.Prefix).Build();
	//	services.AddAuthentication(options =>
	//	{
	//		options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	//		options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	//	}).AddJwtBearer(options =>
	//	{
	//		options.TokenValidationParameters = new TokenValidationParameters()
	//		{
	//			ValidIssuer = configuration.GetValue<string>(JwtConstant.Issuer),
	//			ValidateIssuer = true,
	//			ValidateAudience = false,
	//			ValidateIssuerSigningKey = true,
	//			IssuerSigningKey =
	//				new SymmetricSecurityKey(
	//					Encoding.UTF8.GetBytes(configuration.GetValue<string>(JwtConstant.SecretKey)))
	//		};
	//	});
	//	return services;
	//}

	public static IServiceCollection AddConfigSwagger(this IServiceCollection services)
	{
		services.AddSwaggerGen(options =>
		{
			options.SwaggerDoc("v1", new OpenApiInfo() { Title = "Pos System", Version = "v1" });
			options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
			{
				In = ParameterLocation.Header,
				Description = "Please enter a valid token",
				Name = "Authorization",
				Type = SecuritySchemeType.Http,
				BearerFormat = "JWT",
				Scheme = "Bearer"
			});
			options.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					new string[] { }
				}
			});
			options.MapType<TimeOnly>(() => new OpenApiSchema
			{
				Type = "string",
				Format = "time",
				Example = OpenApiAnyFactory.CreateFromJson("\"13:45:42.0000000\"")
            });
        });
        services.AddControllers().AddJsonOptions(x =>
		{
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        });
		return services;
	}
}