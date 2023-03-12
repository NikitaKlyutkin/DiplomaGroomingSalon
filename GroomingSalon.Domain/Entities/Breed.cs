using System;
using System.ComponentModel.DataAnnotations;
using DiplomaGroomingSalon.Domain.Entities.Interfaces;

namespace DiplomaGroomingSalon.Domain.Entities
{
	public class Breed : IEntity
	{
		[Key]
		public Guid Id { get; set; }
		public string BreedName { get; set; }
		public PetType PetType { get; set; }
		public Guid PetTypeId { get; set; }
        public string Description { get; set; }
    }
}
