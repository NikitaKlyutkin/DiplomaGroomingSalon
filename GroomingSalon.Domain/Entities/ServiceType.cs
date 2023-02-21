using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaGroomingSalon.Domain.Entities
{
	public class ServiceType
	{
		[Key]
		public Guid Id { get; set; }
		public string serviceTypeName { get; set; }
		public decimal Price { get; set; }
		public Breed Breed { get; set; }
		public Guid BreedPetId { get; set; }
		public Guid TypePetId { get; set; }
        public string Description { get; set; }
    }
}
