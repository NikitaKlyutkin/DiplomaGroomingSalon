using System;
using System.ComponentModel.DataAnnotations;
using DiplomaGroomingSalon.Domain.Entities.Interfaces;

namespace DiplomaGroomingSalon.Domain.Entities
{
    public class PetType : IEntity
	{
		[Key]
		public Guid Id { get; set; }
		[Display(Name = "Pet Type")]
		public string PetTypeName { get; set; }
        public string Description { get; set; }
    }
}
