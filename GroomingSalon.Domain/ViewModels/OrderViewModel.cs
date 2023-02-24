using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Enum;
using System;

namespace DiplomaGroomingSalon.Domain.ViewModels
{
	public class OrderViewModel
	{

		public Guid Id { get; set; }
        public StatusOrder StatusOrder { get; set; }
        public Profile Profile { get; set; }
        public Guid ProfileId { get; set; }
		public Appointment Appointment { get; set; }
        public Guid AppointmentId { get; set; }
        public string NamePet { get; set; }
		public Guid PetTypeId { get; set; }
        public Guid BreedId { get; set; }
        public Guid ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public decimal Price { get; set; }
	}
}
