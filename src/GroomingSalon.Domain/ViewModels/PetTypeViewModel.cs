using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaGroomingSalon.Domain.ViewModels
{
	public class PetTypeViewModel
	{
        [Key]
        public Guid Id { get; set; }
        public string PetTypeName { get; set; }
        public Guid PetTypeId { get; set; }
        public string Description { get; set; }
    }
}
