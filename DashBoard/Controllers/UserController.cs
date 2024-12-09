using DashBoard.Models.UserViewModels;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.Controllers
{
	[Authorize(Roles = "Admin")]
	public class UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var users = await userManager.Users.ToListAsync();


            var mappedUsers = users.Select(U => new UserViewModel()
            {
                Id = U.Id,
                Name = U.UserName,
                Email = U.Email,
                Roles = userManager.GetRolesAsync(U).Result
            });


            return View(mappedUsers);
        }
    
        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            var roles = await roleManager.Roles.ToListAsync();
            var mappedRoles = roles.Select( R => new RoleCheckbox() 
            {
                RoleName = R.Name,
                Selected = userManager.IsInRoleAsync(user ,R.Name).Result
            }).ToList();


            var mappedUser = new EditDeleteUserViewModel() 
            {
                Id = user.Id,
                Name = user.UserName,
                DisplayName = user.DisplayName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Roles = mappedRoles
            };

            return View(mappedUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditDeleteUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(id);

            user.UserName = model.Name;
            user.DisplayName = model.DisplayName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            // await userManager.AddToRolesAsync(user, model.Roles.Where(R => R.Selected).Select(R => R.RoleName));

            foreach (var role in model.Roles)
            {
                if (role.Selected && !await userManager.IsInRoleAsync(user, role.RoleName))
                    await userManager.AddToRoleAsync(user, role.RoleName);
                
                if (!role.Selected && await userManager.IsInRoleAsync(user, role.RoleName))
                    await userManager.RemoveFromRoleAsync(user, role.RoleName);
            }

            await userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }
    
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

            var roles = await roleManager.Roles.ToListAsync();
            var mappedRoles = roles.Select(R => new RoleCheckbox()
            {
                RoleName = R.Name,
                Selected = userManager.IsInRoleAsync(user, R.Name).Result
            }).ToList();


            var mappedUser = new EditDeleteUserViewModel()
            {
                Id = user.Id,
                Name = user.UserName,
                DisplayName = user.DisplayName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Roles = mappedRoles
            };

            return View(mappedUser);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user is null)
                return BadRequest();

            await userManager.DeleteAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }
}
