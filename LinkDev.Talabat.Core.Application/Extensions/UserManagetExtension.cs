using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LinkDev.Talabat.Core.Application.Extensions
{
	public static class UserManagetExtension
	{
		public static async Task<ApplicationUser?> FindUserWithAddress(this UserManager<ApplicationUser> userManager, ClaimsPrincipal claimsPrincipal)
		{
			var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
			var user = await userManager.Users.Where(U => U.Email == email).Include(U => U.Address).FirstOrDefaultAsync();
			return user;
		}
	}
}
