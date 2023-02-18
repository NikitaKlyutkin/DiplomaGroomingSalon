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
		public Profile Profile { get; set; }
		public Guid ProfileId { get; set; }
        public Appointment Appointment { get; set; }
		public Guid AppointmentId { get; set; }
		public string NamePet { get; set; }
		public Guid TypePetId { get; set; }
		public Guid BreedPetId { get; set; }
        public Guid ServiceTypeId { get; set; }
		public decimal Price { get; set; }
		public ServiceType ServiceType { get; set; }
    }
}
