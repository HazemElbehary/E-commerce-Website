using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Auth;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LinkDev.Talabat.APIs.Extensions
{
	public static class IdentityExtensions
	{
		public static IServiceCollection AddIdentityService(this IServiceCollection service, IConfiguration configuration)
		{
			service.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

			service.AddIdentity<ApplicationUser, IdentityRole>(IdentityOptions => 
			{
				IdentityOptions.User.RequireUniqueEmail = true;

				IdentityOptions.SignIn.RequireConfirmedAccount = true;
				IdentityOptions.SignIn.RequireConfirmedEmail = true;
				IdentityOptions.SignIn.RequireConfirmedPhoneNumber = true;

				IdentityOptions.Lockout.AllowedForNewUsers = true;
				IdentityOptions.Lockout.MaxFailedAccessAttempts = 10;
				IdentityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

			}).AddEntityFrameworkStores<StoreIdentityDbContext>();


			service.AddAuthentication(Options =>
			{
				Options.DefaultAuthenticateScheme = "Bearer";
				Options.DefaultChallengeScheme = "Bearer";
			})
				.AddJwtBearer((configurationOptions) =>
				{
					configurationOptions.TokenValidationParameters = new TokenValidationParameters() 
					{ 
						ValidateAudience = true,
						ValidateIssuer = true,
						ValidateIssuerSigningKey = true,
						ValidateLifetime = true,

						ClockSkew = TimeSpan.FromMinutes(0),
						ValidIssuer = configuration["JwtSettings:Issure"],
						ValidAudience = configuration["JwtSettings:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)) 
					};
				});

			return service;
		}
	}
}
