using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.Domain.Entities.Interfaces;

namespace DiplomaGroomingSalon.Domain.Entities
{
	public class Breed : IEntity
	{
		[Key]
		public Guid Id { get; set; }
		public string breedPetName { get; set; }
		public PetType PetType { get; set; }
		public Guid PetTypeId { get; set; }
        public string PetTypeName { get; set; }
        public string Description { get; set; }
    }
}
