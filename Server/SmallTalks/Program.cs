



using Core.Entities.Identity;
using Infrastructure.Identity;
using Infrastructure.Identity.IdentityContextSeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmallTalks.ChatHubs;
using SmallTalks.Extensions;

namespace SmallTalks
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
		    builder.AddApplicationServices();
			builder.Services.AddIdentityService(builder.Configuration);
			builder.Services.AddSwaggerDocumentation();
			//builder.Services.AddAuthorization(options =>
			//{
			//	options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("admin"));
			//});
			var app = builder.Build();
			#region DataSeed 
			using (var scope = app.Services.CreateScope())
			{


				var services = scope.ServiceProvider;
				var loggerfactory = services.GetRequiredService<ILoggerFactory>();
				try
				{
					var userManager = services.GetRequiredService<UserManager<AppUser>>();
					var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
					var identityContext = services.GetRequiredService<AppIdentityDbContext>();
					await identityContext.Database.MigrateAsync();
					await AppIdentityDbContextSeed.SeedUserAsync(userManager,roleManager);
				}
				catch (Exception ex)
				{
					var logger = loggerfactory.CreateLogger<Program>();
					logger.LogError(ex, "error occured during migration");
				}


			} 
			#endregion
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseCors("AllowSpecificOrigin");
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.MapHub<ChatHub>("hubs/chat"); 
			
			app.Run();
		}
	}
}