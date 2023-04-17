using DiplomaGroomingSalon.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace DiplomaGroomingSalon.Domain.ViewModels
{
	public class ServiceTypeViewModel
	{
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Service Type")]
		public string ServiceTypeName { get; set; }
		public decimal Price { get; set; }
		public Breed Breed { get; set; }
		public Guid BreedId { get; set; }
        public Guid PetTypeId { get; set; }
        public string PetTypeName { get; set; }
        public string BreedName { get; set; }
        public string Description { get; set; }
    }
}
