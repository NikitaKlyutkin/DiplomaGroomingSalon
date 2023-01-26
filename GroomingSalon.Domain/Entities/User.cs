using System;
using System.Data;
using DiplomaGroomingSalon.Domain.Enum;

namespace DiplomaGroomingSalon.Domain.Entities
{
	public class User
	{
		public Guid Id { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public Role Role { get; set; }
		public Account Account { get; set; }
	}
}
