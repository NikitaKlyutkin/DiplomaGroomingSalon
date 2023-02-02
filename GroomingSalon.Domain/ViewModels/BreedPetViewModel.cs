using DiplomaGroomingSalon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaGroomingSalon.Domain.ViewModels
{
	public class BreedPetViewModel
	{
		public string breedPetName { get; set; }
		public TypePet TypePet { get; set; }
		public Guid TypePetId { get; set; }
    }
}
