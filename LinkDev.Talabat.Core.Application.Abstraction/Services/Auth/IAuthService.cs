using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.DTOs.Order;
using System.Security.Claims;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Auth
{
	public interface IAuthService
	{
		Task<UserDto> LoginAsync(LoginDto model);

		Task<UserDto> RegisterAsync(RegisterDto model);

		Task<UserDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal);

		Task<AddressDto> GetUserAddress(ClaimsPrincipal claimsPrincipal);

		Task<AddressDto> UpdateUserAddress(ClaimsPrincipal claimsPrincipal, AddressDto newAddress);


		Task<bool> EmailExists(string email);

	}
}
