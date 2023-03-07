using System.ComponentModel.DataAnnotations;

namespace DiplomaGroomingSalon.Domain.Enum
{
	public enum Role
	{
		[Display(Name = "User")]
		User = 0,
		[Display(Name = "Admin")]
		Admin = 2,
	}
}
