using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Order;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Account
{
	public class AccountController(IServiceManager serviceManager) : BaseApiController
	{
		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		{
			var result = await serviceManager.AuthService.LoginAsync(model);
			return Ok(result);
		}

		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto model)
		{
			var result = await serviceManager.AuthService.RegisterAsync(model);
			return Ok(result);
		}

		[Authorize]
		[HttpGet]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var result = await serviceManager.AuthService.GetCurrentUser(User);
			return Ok(result);
		}

		[Authorize]
		[HttpGet("address")]
		public async Task<ActionResult<AddressDto>> GetUserAddress()
		{
			var result = await serviceManager.AuthService.GetUserAddress(User);
			return Ok(result);
		}

		[Authorize]
		[HttpPut("address")]
		public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
		{
			var result = await serviceManager.AuthService.UpdateUserAddress(User, addressDto);
			return Ok(result);
		}

		[HttpGet("emailExists")]
		public async Task<ActionResult<bool>> EmailExists(string email) 
		{
			return Ok(await serviceManager.AuthService.EmailExists(email));
		}


	}
}
