using DiplomaGroomingSalon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaGroomingSalon.Domain.ViewModels
{
	public class BreedPetViewModel
	{
        [Key]
        public Guid Id { get; set; }
        public string breedPetName { get; set; }
		public PetType PetType { get; set; }
		public Guid TypePetId { get; set; }
        public string TypePetName { get; set; }
        public string Description { get; set; }
    }
}
