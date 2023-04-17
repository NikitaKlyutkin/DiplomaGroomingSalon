using System;
using System.ComponentModel.DataAnnotations;
using DiplomaGroomingSalon.Domain.Entities.Interfaces;
using DiplomaGroomingSalon.Domain.Enum;

namespace DiplomaGroomingSalon.Domain.Entities
{
	
	public class Order : IEntity
	{
		public Guid Id { get; set; }
        public string Description { get; set; }
        [Display(Name = "Status Order")]
		public StatusOrder StatusOrder { get; set; }
		public Profile Profile { get; set; }
		public Guid ProfileId { get; set; }
        public Appointment Appointment { get; set; }
        public Guid AppointmentId { get; set; }
        [Display(Name = "Nickname Pet")]
		public string NamePet { get; set; }
		public Guid TypePetId { get; set; }
		public Guid BreedPetId { get; set; }
        public Guid ServiceTypeId { get; set; }
		public decimal Price { get; set; }
		public ServiceType ServiceType { get; set; }
    }
}
