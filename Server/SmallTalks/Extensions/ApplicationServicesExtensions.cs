
using Core.Interfaces;
using Infrastructure.Data.Repository;
using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace SmallTalks.Extensions
{
	public static class ApplicationServicesExtensions
	{
		public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			builder.Services.AddScoped<ITokenService, TokenService>();
			builder.Services.AddScoped<IAzureBlobService, AzureBlobService>();
			builder.Services.AddDbContext<AppIdentityDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));
			builder.Services.AddSignalR();
			//builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200")));
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowSpecificOrigin", builder =>
				{
					builder
						.WithOrigins("http://localhost:4200")  // Replace with the actual origin of your Angular app
						.AllowAnyHeader()
						.AllowAnyMethod();
				});
			});

			return builder;
		}

	}
}
