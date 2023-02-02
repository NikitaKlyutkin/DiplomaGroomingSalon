using DiplomaGroomingSalon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaGroomingSalon.Domain.ViewModels
{
	public class ServiceTypeViewModel
	{
		public string serviceTypeName { get; set; }
		public decimal Price { get; set; }
		public Guid BreedPetId { get; set; }
        public Guid TypePetId { get; set; }
	}
}
