using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
