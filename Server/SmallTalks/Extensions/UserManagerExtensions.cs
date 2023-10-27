using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace SmallTalks.Extensions
{
	public static class UserManagerExtensions
	{
		public static async Task<AppUser> FindUserByClaimsPrincipleWithAddressAsync(this UserManager<AppUser> input, ClaimsPrincipal user)
		{
			var email = user.FindFirstValue(ClaimTypes.Email);
			return await input.Users.Include(x => x.Address).SingleOrDefaultAsync(x=> x.Email==email);

		}

		public static async Task<AppUser> FindUserByEmailFromClaimsPrincipleAsync(this UserManager<AppUser> input, ClaimsPrincipal user)
		{
			var email = user.FindFirstValue(ClaimTypes.Email);
			return await input.Users.SingleOrDefaultAsync(x => x.Email == email);
		}
		public static async Task<IEnumerable<string>> GetAllUserNamesAsync(this UserManager<AppUser> users)
		{
			return await users.Users.Select(x => x.DisplayName).ToListAsync();
		}



	}
}
