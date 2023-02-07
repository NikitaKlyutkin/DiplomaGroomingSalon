using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DiplomaGroomingSalon.Domain.Entities
{
	public class Account
	{
		[Key]
		[ForeignKey("User")]
		public Guid Id { get; set; }
		public string Name { get; set;}
		public string Surname { get; set;}
		public string Email { get; set;}
		public string Phone { get; set;}
		public string NamePet { get; set;}
		public string BreedPet { get;set;}
        public string Description { get; set; }
        public User User { get; set; }
		public List<Order> Orders { get; set; }
	}
}
