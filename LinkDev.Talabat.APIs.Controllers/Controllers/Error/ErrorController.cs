using LinkDev.Talabat.APIs.Controllers.Errors;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Error
{
    [ApiController]
    [Route("Error/{Code}")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ErrorController : ControllerBase
    {

        [HttpGet]
        public IActionResult Error(int Code)

        {
            if (Code == 404)
            {
                var response = new ApiResponce(Code, $"The Requested End Point Is Not Found");
                return NotFound(response);
            }

            return StatusCode(Code, new ApiResponce(Code));
        }
    }
}
