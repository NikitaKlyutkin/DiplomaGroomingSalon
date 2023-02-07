using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaGroomingSalon.Domain.Entities
{
	public class BreedPet
	{
		[Key]
		public Guid IdBreedPet { get; set; }
		public string breedPetName { get; set; }
		public TypePet TypePet { get; set; }
		public Guid TypePetId { get; set; }
        public string Description { get; set; }
    }
}
