using System;
using System.ComponentModel.DataAnnotations;

namespace DiplomaGroomingSalon.Domain.ViewModels
{
	public class PetTypeViewModel
	{
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "Pet Type")]
		public string PetTypeName { get; set; }
        public Guid PetTypeId { get; set; }
        public string Description { get; set; }
    }
}
