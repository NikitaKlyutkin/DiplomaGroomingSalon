using System;
using System.Collections.Generic;
using DiplomaGroomingSalon.Domain.Entities.Interfaces;

namespace DiplomaGroomingSalon.Domain.Entities
{
	public class Profile : IEntity
	{
		public Guid Id { get; set; }
		public string Name { get; set;}
		public string Surname { get; set;}
		public string Email { get; set;}
		public string Phone { get; set;}
		public string Description { get; set; }
        public User User { get; set; }
		public Guid UserId { get; set; }
        public List<Order> Orders { get; set; }
        public string UserName { get; set; }
    }
}
