using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Enum;
using DiplomaGroomingSalon.Domain.Response;
using DiplomaGroomingSalon.Domain.ViewModels;
using DiplomaGroomingSalon.Service.Interfaces;

namespace DiplomaGroomingSalon.Service.Implementations
{
    public class AppointmentService : IAppointmentService
    {
	    private readonly IBaseRepository<Appointment> _appointmentRepository;

	    public AppointmentService(IBaseRepository<Appointment> appointmentRepository)
	    {
			_appointmentRepository = appointmentRepository;
	    }

	    public async Task<IBaseResponse<AppointmentViewModel>> CreateAppointment(AppointmentViewModel appointmentViewModel)
	    {
			var baseResponse = new BaseResponse<AppointmentViewModel>();
			try
			{
				var appointment = new Appointment()
				{
					Id = Guid.NewGuid(),
					DateTimeAppointment = appointmentViewModel.DateTimeAppointment,
					StatusAppointment = true
				};

				await _appointmentRepository.Create(appointment);

			}
			catch (Exception ex)
			{
				return new BaseResponse<AppointmentViewModel>()
				{
					Description = $"[CreateAppointment]: {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
			return baseResponse;
	    }

		public async Task<IBaseResponse<List<Appointment>>> GetAppointments()
		{
		    var baseResponse = new BaseResponse<IEnumerable<Appointment>>();
		    try
		    {
                var appointmentsData = await _appointmentRepository.GetAll();
                var appointments = appointmentsData.ToList();
			    var appointmentsTrue = new List<Appointment>();
				
			    if (!appointments.Any())
				{
					return new BaseResponse<List<Appointment>>()
					{
						Description = "Found 0 items",
						StatusCode = StatusCode.OK
					};
				}

				foreach (var appointment in appointments)
				{
					if (appointment.StatusAppointment == true)
					{
						appointmentsTrue.Add(appointment);
					}
				}
				return new BaseResponse<List<Appointment>>()
				{
					Data = appointmentsTrue,
					StatusCode = StatusCode.OK
				};
			}
		    catch (Exception ex)
		    {
			    return new BaseResponse<List<Appointment>>()
			    {
				    Description = $"[GetAppointments] : {ex.Message}",
				    StatusCode = StatusCode.InternalServerError
			    };
			}
	    }
    }
}
