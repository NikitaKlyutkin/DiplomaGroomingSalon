using System.ComponentModel.DataAnnotations;

namespace DiplomaGroomingSalon.Domain.Enum
{
	public enum StatusOrder
	{
		[Display(Name = "Сompleted")]
		Сompleted = 0,
		[Display(Name = "Not done")]
		NotDone = 1,
		[Display(Name = "During")]
		During = 2
	}
}
