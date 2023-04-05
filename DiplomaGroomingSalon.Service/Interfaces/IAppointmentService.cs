using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Response;
using DiplomaGroomingSalon.Domain.ViewModels;

namespace DiplomaGroomingSalon.Service.Interfaces;

public interface IAppointmentService
{
	Task<IBaseResponse<AppointmentViewModel>> CreateAppointment(AppointmentViewModel appointmentViewModel);
	Task<IBaseResponse<List<Appointment>>> GetAppointmentsFree();
	Task<IBaseResponse<Appointment>> UpdateAppointment(Guid id, Appointment model);
	Task<IBaseResponse<Appointment>> GetById(Guid id);
	Task<IBaseResponse<bool>> Delete(Guid id);
}