using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;

namespace DashBoard.Controllers
{
    public class BrandController(IServiceManager serviceManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var brands = await serviceManager.ProductService.GetBrandsAsync();

            return View(brands);
        }
    
        public async Task<IActionResult> Delete(int id)
        {
            await serviceManager.ProductService.DeleteBrand(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
