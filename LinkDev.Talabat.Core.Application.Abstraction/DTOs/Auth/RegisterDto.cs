using System.ComponentModel.DataAnnotations;

namespace LinkDev.Talabat.Core.Application.Abstraction.DTOs.Auth
{
	public class RegisterDto
	{
        [Required]
		public required string DisplayName { get; set; }

        [Required]
        public required string UserName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string PhoneNumber { get; set; }

        [Required]
		//[RegularExpression(
  //          pattern: "^.(6,10}$) (?=.*\\d) (?=.*[a-z]) (?=.*[A-Z]) (?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?=.*\\s).*$", 
  //          ErrorMessage = "Password Must Has 1 UperCase, 1 LowerCase, 1 Number, 1 NonAlphaNumeric and at Least 6 Characters"
  //       )]
		public required string Password { get; set; }
    }
}
