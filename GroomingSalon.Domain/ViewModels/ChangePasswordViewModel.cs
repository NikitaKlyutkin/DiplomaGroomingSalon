using System.ComponentModel.DataAnnotations;

namespace DiplomaGroomingSalon.Domain.ViewModels
{
	public class ChangePasswordViewModel
	{
		[Required(ErrorMessage = "Enter a name")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Enter password")]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		[MinLength(5, ErrorMessage = "Password must be greater than or equal to 5 characters")]
		public string NewPassword { get; set; }
	}
}
