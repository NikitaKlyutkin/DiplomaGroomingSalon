﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaGroomingSalon.Domain.Entities
{
	public class Appointment
	{
		public Guid Id { get; set; }
		[DataType(DataType.Date)]
        public DateTime DateAppointment { get; set; }
		[DataType(DataType.Time)]
		public DateTime TimeAppointment { get; set; }
		public bool StatusAppointment { get; set; }
	}
}
