using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.DAL.Repositories;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Enum;
using DiplomaGroomingSalon.Domain.Response;
using DiplomaGroomingSalon.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace DiplomaGroomingSalon.Service.Implementations
{
	public class OrderService : IOrderService
	{
		private readonly IBaseRepository<Order> _orderRepository;
		private readonly IBaseRepository<Appointment> _appointmentRepository;
		public OrderService(IBaseRepository<Order> orderRepository, IBaseRepository<Appointment> appointmentRepository)
		{
			_orderRepository = orderRepository;
			_appointmentRepository = appointmentRepository;
		}
		public async Task<IBaseResponse<OrderViewModel>> CreateOrder(OrderViewModel orderViewModel)
		{
			var baseResponse = new BaseResponse<OrderViewModel>();
			try
			{

				var order = new Order()
				{
					Id = new Guid(),
					StatusOrder = StatusOrder.During,
					Account = orderViewModel.Account,
					
					AppointmentId = orderViewModel.AppointmentId
				};

				var appointmentRepository = _appointmentRepository.GetAll()
					.FirstOrDefault(x => x.Id == orderViewModel.AppointmentId);
				var appointment = new Appointment()
				{
					Id = order.AppointmentId,
					DateTimeAppointment = appointmentRepository!.DateTimeAppointment,
					StatusAppointment = appointmentRepository.StatusAppointment = false
				};

				await _orderRepository.Create(order);
				await _appointmentRepository.Update(appointment);

			}
			catch (Exception ex)
			{
				return new BaseResponse<OrderViewModel>()
				{
					Description = $"[CreateAppointment]: {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
			return baseResponse;
		}
	}
}
