using System;

namespace DiplomaGroomingSalon.Domain.ViewModels
{
	public class AppointmentViewModel
	{
        public Guid Id { get; set; }
        public DateTime DateTimeAppointment { get; set; }
        public string Description { get; set; }
    }
}
