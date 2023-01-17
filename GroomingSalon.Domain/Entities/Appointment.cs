using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaGroomingSalon.Domain.Entities
{
	public class Appointment
	{
		public Guid Id { get; set; }
		public DateTime dateTime { get; set; }
	}
}
