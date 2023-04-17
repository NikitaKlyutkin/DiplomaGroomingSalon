using DiplomaGroomingSalon.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace DiplomaGroomingSalon.Domain.ViewModels
{
	public class BreedViewModel
	{
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Breed")]
		public string BreedName { get; set; }
		public PetType PetType { get; set; }
		public Guid PetTypeId { get; set; }
        public string TypePetName { get; set; }
        public string Description { get; set; }
    }
}
