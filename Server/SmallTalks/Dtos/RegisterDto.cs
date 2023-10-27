using System.ComponentModel.DataAnnotations;

namespace SmallTalks.Dtos
{
	public class RegisterDto
	{
		[Required]
		public string DisplayName { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		[RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",ErrorMessage = "Password must be 6 to 10 characters long and include at least one digit, one lowercase letter, one uppercase letter, one non-alphanumeric character, and no spaces.")]
		public string Password { get; set; }
	}
}
