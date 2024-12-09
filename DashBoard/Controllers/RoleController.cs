using DashBoard.Models.RoleViewModels;
using LinkDev.Talabat.Core.Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.Controllers
{
	[Authorize(Roles = "Admin")]
	public class RoleController(RoleManager<IdentityRole> roleManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var roles = await roleManager.Roles.ToListAsync();

            var mappedRoles = roles.Select(R => new RoleViewModel() 
            { 
                Id = R.Id,
                Name = R.Name!
            });

            return View(mappedRoles);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            IdentityRole role = new IdentityRole(name);

            try
            {
                await roleManager.CreateAsync(role);
            }
            catch
            {
                throw new Exception();
            }
            
            return RedirectToAction(nameof(Index));
        }
    
        public async Task<IActionResult> Edit(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if(role is null)
                throw new _NotFoundException("Role", id);

            var mappedRole = new RoleViewModel() 
            { Id = role.Id, Name = role.Name! };

            return View(mappedRole);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            role!.Name = model.Name;

            await roleManager.UpdateAsync(role);

            return RedirectToAction(nameof(Index));
        }
    
        public async Task<IActionResult> Delete(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role is null)
                throw new _NotFoundException("Role", id);

            var mappedRole = new RoleViewModel()
            { Id = role.Id, Name = role.Name! };

            return View(mappedRole);
        }

        [HttpPost]
        public async Task<IActionResult> PostDelete(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role is null)
                throw new _NotFoundException("Role", id);

            try
            {
                await roleManager.DeleteAsync(role);
            }
            catch (Exception)
            {
                throw new Exception();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
