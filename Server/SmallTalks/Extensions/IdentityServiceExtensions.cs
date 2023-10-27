using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SmallTalks.Extensions
{
	public static class IdentityServiceExtensions
	{
		public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration config)
		{
			//services.AddIdentity<AppUser, IdentityRole>()
			//	.AddEntityFrameworkStores<AppIdentityDbContext>();

			services.AddIdentityCore<AppUser>()
			.AddEntityFrameworkStores<AppIdentityDbContext>()
			 .AddSignInManager<SignInManager<AppUser>>();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
					ValidIssuer = config["Token:Issuer"],
					ValidateIssuer = true,
					ValidateAudience = false
				};
			});
			services.AddAuthorization();

			return services;
		}

		//public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration config) 
		//{
		//	var builder = services.AddIdentityCore<AppUser>();
		//	builder = new IdentityBuilder(builder.UserType, builder.Services );
		//	builder.AddEntityFrameworkStores<AppIdentityDbContext>();
		//	builder.AddSignInManager<SignInManager<AppUser>>();
		//	services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=> options.TokenValidationParameters= new TokenValidationParameters()
		//	{
		//		ValidateIssuerSigningKey = true,
		//		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
		//		ValidIssuer = config["Token:Issuer"],
		//		ValidateIssuer = true,
		//		ValidateAudience = false

		//	});
		//	services.AddIdentity<AppUser, IdentityRole>()
		//    .AddEntityFrameworkStores<AppIdentityDbContext>()
		//    .AddDefaultTokenProviders();
		//	return services;

		//}
	}
}
