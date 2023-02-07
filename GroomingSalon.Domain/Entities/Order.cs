using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.Domain.Enum;

namespace DiplomaGroomingSalon.Domain.Entities
{
	
	public class Order 
	{
		public Guid Id { get; set; }
		public StatusOrder StatusOrder { get; set; }
		public Account Account { get; set; }
		public Guid AppointmentId { get; set; }
		public Appointment Appointment { get; set; }
        public string Description { get; set; }
    }
}
