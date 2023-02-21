using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.Domain.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DiplomaGroomingSalon.Domain.Entities
{
    public class PetType : IEntity
	{
		[Key]
		public Guid Id { get; set; }
		public string typePetName { get; set; }
        public string Description { get; set; }
    }
}
