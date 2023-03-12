using System;
using System.ComponentModel.DataAnnotations;
using DiplomaGroomingSalon.Domain.Entities.Interfaces;

namespace DiplomaGroomingSalon.Domain.Entities
{
	public class ServiceType : IEntity
	{
		[Key]
		public Guid Id { get; set; }
		public string ServiceTypeName { get; set; }
		public decimal Price { get; set; }
		public Breed Breed { get; set; }
		public Guid BreedId { get; set; }
		public Guid PetTypeId { get; set; }
        public string Description { get; set; }
    }
}
