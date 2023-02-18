using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.DAL.Repositories;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Enum;
using DiplomaGroomingSalon.Domain.Response;
using DiplomaGroomingSalon.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DiplomaGroomingSalon.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.VisualBasic;

namespace DiplomaGroomingSalon.Service.Implementations
{
	public class OrderService : IOrderService
	{
		private readonly IBaseRepository<Order> _orderRepository;
		private readonly IBaseRepository<Appointment> _appointmentRepository;
		private readonly IBaseRepository<Profile> _profileRepository;
		private readonly IBaseRepository<TypePet> _typePetRepository;
		public OrderService(IBaseRepository<Order> orderRepository, IBaseRepository<Appointment> appointmentRepository, IBaseRepository<TypePet> typePetRepository, IBaseRepository<Profile> profileRepository)
		{
			_orderRepository = orderRepository;
			_appointmentRepository = appointmentRepository;
			_typePetRepository = typePetRepository;
			_profileRepository = profileRepository;
		}
		public IBaseResponse<List<Order>> GetOrdersAll()
		{
			var baseResponse = new BaseResponse<IEnumerable<Order>>();
			try
			{
				var orders = _orderRepository.GetAll().Include(x => x.ServiceType).ToList();
				if (!orders.Any())
				{
					return new BaseResponse<List<Order>>()
					{
						Description = "Found 0 items",
						StatusCode = StatusCode.OK
					};
				}
				return new BaseResponse<List<Order>>()
				{
					Data = orders,
					StatusCode = StatusCode.OK
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<List<Order>>()
				{
					Description = $"[GetOrders] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}

		}
		public IBaseResponse<List<Order>> GetOrdersByUser()
		{
			var baseResponse = new BaseResponse<IEnumerable<Order>>();
			try
			{
				var orders = _orderRepository.GetAll().Include(x => x.ServiceType).ToList();
				if (!orders.Any())
				{
					return new BaseResponse<List<Order>>()
					{
						Description = "Found 0 items",
						StatusCode = StatusCode.OK
					};
				}
				return new BaseResponse<List<Order>>()
				{
					Data = orders,
					StatusCode = StatusCode.OK
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<List<Order>>()
				{
					Description = $"[GetOrders] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}

		}
        public async Task<BaseResponse<ProfileViewModel>> GetProfileOrder(string userName)
        {
            try
            {
                var profile = await _profileRepository.GetAll()
                    .Select(x => new ProfileViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Surname = x.Surname,
                        Phone = x.Phone,
                        Email = x.Email,
                        UserName = x.User.Name
                    })
                    .FirstOrDefaultAsync(x => x.UserName == userName);

                return new BaseResponse<ProfileViewModel>()
                {
                    Data = profile,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProfileViewModel>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
        public async Task<IBaseResponse<OrderViewModel>> CreateOrder(OrderViewModel orderViewModel, Guid ProfileId)
        {
            var baseResponse = new BaseResponse<OrderViewModel>();
			try
			{

				var order = new Order()
				{
					Id = new Guid(),
					StatusOrder = StatusOrder.During,
					Profile = orderViewModel.Profile,
					NamePet = orderViewModel.NamePet,
					ProfileId = ProfileId,
                    AppointmentId = orderViewModel.AppointmentId,
					TypePetId = orderViewModel.TypePetId,
					BreedPetId = orderViewModel.BreedPetId,
					ServiceTypeId = orderViewModel.ServiceTypeId,
					Price = orderViewModel.Price
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
