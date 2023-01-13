using System;
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

		public User User { get; set; }
	}
}
