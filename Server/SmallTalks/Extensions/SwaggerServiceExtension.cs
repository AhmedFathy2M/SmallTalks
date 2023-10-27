using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace SmallTalks.Extensions
{
	public static class SwaggerServiceExtension
	{
		public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
		{

			services.AddSwaggerGen(c => 
			{
				var securitySchema = new OpenApiSecurityScheme()
				{
					Description = "JWT Auth Bearer Scheme",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer",
					Reference = new OpenApiReference()
					{
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					}

				};
				c.AddSecurityDefinition("Bearer",securitySchema);
				var securityRequirement = new OpenApiSecurityRequirement() 
				{
					{securitySchema, new[]{ "Bearer"} }
				};
				c.AddSecurityRequirement(securityRequirement);
			});
			
			return services;
		}
	}
}
