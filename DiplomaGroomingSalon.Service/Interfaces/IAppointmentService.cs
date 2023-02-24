using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Response;
using DiplomaGroomingSalon.Domain.ViewModels;

namespace DiplomaGroomingSalon.Service.Interfaces
{
    public interface IAppointmentService
    {
	    Task<IBaseResponse<AppointmentViewModel>> CreateAppointment(AppointmentViewModel appointmentViewModel);
        Task<IBaseResponse<List<Appointment>>> GetAppointments();



    }
}
