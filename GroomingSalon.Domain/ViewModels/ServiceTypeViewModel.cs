using DiplomaGroomingSalon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaGroomingSalon.Domain.ViewModels
{
	public class ServiceTypeViewModel
	{
        [Key]
        public Guid Id { get; set; }
        public string serviceTypeName { get; set; }
		public decimal Price { get; set; }
		public Breed Breed { get; set; }
		public Guid BreedId { get; set; }
        public Guid PetTypeId { get; set; }
        public string TypePetName { get; set; }
        public string BreedPetName { get; set; }
        public string Description { get; set; }
    }
}
