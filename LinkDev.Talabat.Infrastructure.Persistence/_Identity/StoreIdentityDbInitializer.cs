using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Common;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.Infrastructure.Persistence._Identity
{
    public class StoreIdentityDbInitializer(StoreIdentityDbContext context, UserManager<ApplicationUser> _userManager) : DbInitializer(context),  IStoreIdentityDbInitializer
	{
		public async override Task SeedAsync()
		{
			if (!_userManager.Users.Any())
			{
				ApplicationUser user = new ApplicationUser()
				{
					DisplayName = "Hazem Ahmed",
					UserName = "Hazem.Ahmed",
					Email = "Hazem.Ahmed@gmail.com",
					PhoneNumber = "01122334455"
				};

				await _userManager.CreateAsync(user, "P@ssw0rd"); 
			}
		}
	}
}