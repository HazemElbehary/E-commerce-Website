using LinkDev.Talabat.APIs.Controllers.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Error
{
    public class BuggyController : BaseApiController
    {

        [HttpGet("notFound")] // GET: /api/buggy/notFound
        public IActionResult GetNotFoundRequest()
        {
            // throw new NotFoundException();
            return NotFound(new ApiResponce(404)); // 404
        }


        [HttpGet("serverError")] // GET: /api/buggy/serverError
        public IActionResult GetServerError()
        {
            throw new Exception(); // 500
        }


        [HttpGet("badRequest")] // GET: /api/buggy/badRequest
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponce(400)); // 400
        }


        [HttpGet("unathorizedError")] // // GET: /api/buggy/unathorizedError
        public IActionResult GetUnAuthorizedError()
        {
            return Unauthorized(new ApiResponce(401));
        }


        [HttpGet("validationError/{id}")] // // GET: /api/buggy/validationError/Five
        public IActionResult GetValidationError(int id)
        {
            return Ok();
        }


        [HttpGet("foribidden")] // GET: /api/buggy/foribidden
        public IActionResult GetForbiddenRequest()
        {
            return Forbid();
        }


        [HttpGet("unAuthorizedRequest")] // GET: /api/buggy/unAuthorizedRequest
        [Authorize]
        public IActionResult GetAuhtorizedRequest()
        {
            return Ok();
        }
    }
}
