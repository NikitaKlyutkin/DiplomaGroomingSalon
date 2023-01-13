using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.Domain.Enum;

namespace DiplomaGroomingSalon.Domain.Entities
{
	[Table("Orders")]
	public class Order : Account
	{
		public StatusOrder StatusOrder { get; set; }
		public Account Account { get; set; }
	}
}
