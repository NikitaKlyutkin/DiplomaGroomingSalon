﻿using System;
using System.Data;
using DiplomaGroomingSalon.Domain.Enum;

namespace DiplomaGroomingSalon.Domain.Entities
{
	public class User
	{
		
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
		public Role Role { get; set; }
		public Profile Profile { get; set; }
		public Guid ProfileId { get; set; }
		public string Description { get; set; }

    }
}
