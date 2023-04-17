using System;
using System.ComponentModel.DataAnnotations;
using DiplomaGroomingSalon.Domain.Entities.Interfaces;

namespace DiplomaGroomingSalon.Domain.Entities
{
	public class Appointment : IEntity
	{
		public Guid Id { get; set; }
		[Display(Name = "Date and Time Appointment")]
		public DateTime DateTimeAppointment { get; set; }
		public bool StatusAppointment { get; set; }
		public string Description { get; set; }
    }
}
