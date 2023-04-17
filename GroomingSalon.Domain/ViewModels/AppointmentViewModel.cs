using System;
using System.ComponentModel.DataAnnotations;

namespace DiplomaGroomingSalon.Domain.ViewModels
{
	public class AppointmentViewModel
	{
        public Guid Id { get; set; }
        [Display(Name = "Date and Time Appointment")]
		public DateTime DateTimeAppointment { get; set; }
        public string Description { get; set; }
    }
}
