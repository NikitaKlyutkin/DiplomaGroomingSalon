using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaGroomingSalon.Domain.ViewModels
{
	public class OrderViewModel
	{
        public Guid Id { get; set; }
        public Account Account { get; set; }
		public Guid AppointmentId { get; set; }
		public Appointment Appointment { get; set; }
        public string Description { get; set; }
    }
}
