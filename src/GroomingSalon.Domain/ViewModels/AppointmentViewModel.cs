using DiplomaGroomingSalon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaGroomingSalon.Domain.ViewModels
{
	public class AppointmentViewModel
	{
        public Guid Id { get; set; }
        public DateTime DateTimeAppointment { get; set; }
        public string Description { get; set; }
    }
}
