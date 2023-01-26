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

namespace DiplomaGroomingSalon.Service.Implementations
{
	public class OrderService : IOrderService
	{
		private readonly IBaseRepository<Order> _orderRepository;

		public OrderService(IBaseRepository<Order> orderRepository)
		{
			_orderRepository = orderRepository;
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
					Appointments = orderViewModel.Appointments
				};

				await _orderRepository.Create(order);

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
