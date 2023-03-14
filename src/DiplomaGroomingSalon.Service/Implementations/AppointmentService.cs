using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.DAL.Repositories;
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

		public async Task<IBaseResponse<List<Appointment>>> GetAppointmentsFree()
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
					if (appointment.StatusAppointment == true && appointment.DateTimeAppointment > DateTime.Now.AddHours(2))
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
				    Description = $"[GetAppointmentsFree] : {ex.Message}",
				    StatusCode = StatusCode.InternalServerError
			    };
			}
	    }
		public async Task<IBaseResponse<Appointment>> UpdateAppointment(Guid id,Appointment model)
		{
			try
			{
				var appointmentAsync = await _appointmentRepository.GetByIdAsync(id);
				if (appointmentAsync == null)
				{
					return new BaseResponse<Appointment>
					{
						Description = "Not found",
						StatusCode = StatusCode.NotFound
					};
				}
				appointmentAsync.Id = model.Id;
				appointmentAsync.DateTimeAppointment = model.DateTimeAppointment;
				appointmentAsync.StatusAppointment = model.StatusAppointment;
				appointmentAsync.Description = model.Description;
				await _appointmentRepository.Update(appointmentAsync);
				return new BaseResponse<Appointment>
				{
					Data = appointmentAsync,
					StatusCode = StatusCode.OK
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<Appointment>
				{
					Description = $"[UpdateAppointment] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
		}

		public async Task<IBaseResponse<Appointment>> GetById(Guid id)
		{
			try
			{
				var appointmentAsync = await _appointmentRepository.GetByIdAsync(id);
				if (appointmentAsync == null)
					return new BaseResponse<Appointment>
					{
						Description = "Not Found",
						StatusCode = StatusCode.NotFound
					};

				var data = new Appointment()
				{
					Id = appointmentAsync.Id,
					DateTimeAppointment = appointmentAsync.DateTimeAppointment,
					StatusAppointment = appointmentAsync.StatusAppointment,
					Description = appointmentAsync.Description
				};

				return new BaseResponse<Appointment>
				{
					StatusCode = StatusCode.OK,
					Data = data
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<Appointment>
				{
					Description = $"[GetAppointment] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
		}

		public async Task<IBaseResponse<bool>> Delete(Guid id)
		{
			try
			{
				var appointment = await _appointmentRepository.GetByIdAsync(id);
				if (appointment == null)
				{
					return new BaseResponse<bool>()
					{
						Description = "Not found",
						StatusCode = StatusCode.NotFound,
						Data = false
					};
				}
				else
				{
					await _appointmentRepository.Delete(appointment);
				}


				return new BaseResponse<bool>()
				{
					Data = true,
					StatusCode = StatusCode.OK
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<bool>()
				{
					Description = $"[DeleteAppointment] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
		}
	}
}
