using System.ComponentModel.DataAnnotations;

namespace DiplomaGroomingSalon.Domain.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "Enter a name")]
		[MaxLength(20, ErrorMessage = "The name must be less than 20 characters long")]
		[MinLength(3, ErrorMessage = "The name must be longer than 3 characters")]
		public string Name { get; set; }
		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Enter a password")]
		[MinLength(6, ErrorMessage = "Password must be longer than 6 characters")]
		public string Password { get; set; }
		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Confirm the password")]
		[Compare("Password", ErrorMessage = "Password mismatch")]
		public string PasswordConfirm { get; set; }
	}
}
