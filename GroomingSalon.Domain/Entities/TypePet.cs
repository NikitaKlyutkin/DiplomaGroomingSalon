using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DiplomaGroomingSalon.Domain.Entities
{
	public class TypePet
	{
		[Key]
		public Guid IdTypePet { get; set; }
		public string typePetName { get; set; }
		//public Guid TypePetId { get; set; }
        public string Description { get; set; }
    }
}
