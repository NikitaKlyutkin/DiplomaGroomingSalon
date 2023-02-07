using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaGroomingSalon.Domain.Entities
{
	public class Appointment
	{
		public Guid Id { get; set; }
		
        public DateTime DateTimeAppointment { get; set; }

		public bool StatusAppointment { get; set; }
        public string Description { get; set; }
    }
}
