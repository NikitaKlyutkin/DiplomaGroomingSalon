﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DiplomaGroomingSalon.Domain.Entities.Interfaces;

namespace DiplomaGroomingSalon.Domain.Entities
{

	public class Profile : IEntity
	{
		public Guid Id { get; set; }
		public string Name { get; set;}
		public string Surname { get; set;}
		[Display(Name = "E-mail")]
		[EmailAddress]
		public string Email { get; set;}
		[Display(Name = "Phone Number")]
		[Phone]
		public string Phone { get; set;}
		public string Description { get; set; }
        public User User { get; set; }
		public Guid UserId { get; set; }
        public List<Order> Orders { get; set; }
        public string UserName { get; set; }
		public int VerificationCode { get; set; }
    }
}
