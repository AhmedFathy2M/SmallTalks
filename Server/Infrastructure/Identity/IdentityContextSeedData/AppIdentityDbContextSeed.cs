using System;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Core.Entities.Identity;

namespace Infrastructure.Identity.IdentityContextSeedData
{
	public class AppIdentityDbContextSeed
	{
		public static async Task SeedUserAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			if (!userManager.Users.Any())
			{
				// Create a new user with the "admin" role
				var user1 = new AppUser
				{
					DisplayName = "Ahmed",
					Email = "ahmedfathymohamed1998@gmail.com",
					UserName = "ahmedfathymohamed1998@gmail.com",
					Address = new Address
					{
						FirstName = "Ahmed",
						LastName = "Fathy",
						Street = "6th street",
						City = "Cairo",
						State = "Giza",
						ZipCode = "3387722"
					}
				};
				if (!await roleManager.RoleExistsAsync("admin"))
				{
					await roleManager.CreateAsync(new IdentityRole("admin"));
				}
				await userManager.CreateAsync(user1, "AhmedFathy_2m");
				await userManager.AddToRoleAsync(user1, "admin");
			
			

			
				var user2 = new AppUser
				{
					DisplayName = "Mohamed",
					Email = "ahmedfathymohamed@hotmail.com",
					UserName = "ahmedfathymohamed@hotmail.com",
					Address = new Address
					{
						FirstName = "Mohamed",
						LastName = "Fathy",
						Street = "6th street",
						City = "Cairo",
						State = "Giza",
						ZipCode = "3387733"
					}
				};
				await userManager.CreateAsync(user2, "AhmedFathy_3m");

				var user3 = new AppUser
				{
					DisplayName = "Fathy",
					Email = "fathy@hotmail.com",
					UserName = "fathy@hotmail.com",
					Address = new Address
					{
						FirstName = "Fathy",
						LastName = "Mohamed",
						Street = "6th street",
						City = "Cairo",
						State = "Giza",
						ZipCode = "3387733"
					}
				};
				await userManager.CreateAsync(user3, "AhmedFathy_4m");

				var user4 = new AppUser
				{
					DisplayName = "Kamel",
					Email = "kamel@hotmail.com",
					UserName = "kamel@hotmail.com",
					Address = new Address
					{
						FirstName = "Kamel",
						LastName = "Mohamed",
						Street = "6th street",
						City = "Cairo",
						State = "Giza",
						ZipCode = "3387733"
					}
				};
				await userManager.CreateAsync(user4, "AhmedFathy_5m");


				var user5 = new AppUser
				{
					DisplayName = "Tareq",
					Email = "tareq@hotmail.com",
					UserName = "tareq@hotmail.com",
					Address = new Address
					{
						FirstName = "Tareq",
						LastName = "Mohamed",
						Street = "6th street",
						City = "Cairo",
						State = "Giza",
						ZipCode = "3387733"
					}
				};
				await userManager.CreateAsync(user5, "AhmedFathy_6m");

				var user6 = new AppUser
				{
					DisplayName = "Sameh",
					Email = "sameh@hotmail.com",
					UserName = "sameh@hotmail.com",
					Address = new Address
					{
						FirstName = "Sameh",
						LastName = "Mohamed",
						Street = "6th street",
						City = "Cairo",
						State = "Giza",
						ZipCode = "3387733"
					}
				};
				await userManager.CreateAsync(user6, "AhmedFathy_7m");

				var user7 = new AppUser
				{
					DisplayName = "Fahmy",
					Email = "fahmy@hotmail.com",
					UserName = "fahmy@hotmail.com",
					Address = new Address
					{
						FirstName = "Fahmy",
						LastName = "Mohamed",
						Street = "6th street",
						City = "Cairo",
						State = "Giza",
						ZipCode = "3387733"
					}
				};
				await userManager.CreateAsync(user7, "AhmedFathy_8m");

				var user8 = new AppUser
				{
					DisplayName = "Ramy",
					Email = "ramy@hotmail.com",
					UserName = "ramy@hotmail.com",
					Address = new Address
					{
						FirstName = "Ramy",
						LastName = "Mohamed",
						Street = "6th street",
						City = "Cairo",
						State = "Giza",
						ZipCode = "3387733"
					}
				};
				await userManager.CreateAsync(user8, "AhmedFathy_9m");

				var user9 = new AppUser
				{
					DisplayName = "Medhat",
					Email = "medhat@hotmail.com",
					UserName = "medhat@hotmail.com",
					Address = new Address
					{
						FirstName = "Medhat",
						LastName = "Mohamed",
						Street = "6th street",
						City = "Cairo",
						State = "Giza",
						ZipCode = "3387733"
					}
				};
				await userManager.CreateAsync(user9, "AhmedFathy_10m");

				var user10 = new AppUser
				{
					DisplayName = "George",
					Email = "george@hotmail.com",
					UserName = "george@hotmail.com",
					Address = new Address
					{
						FirstName = "George",
						LastName = "Mohamed",
						Street = "6th street",
						City = "Cairo",
						State = "Giza",
						ZipCode = "3387733"
					}
				};
				await userManager.CreateAsync(user10, "AhmedFathy_11m");

			}
		}
	}
}