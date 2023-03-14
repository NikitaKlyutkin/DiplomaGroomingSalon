using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.DAL.Repositories;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Enum;
using DiplomaGroomingSalon.Domain.Response;
using DiplomaGroomingSalon.Domain.ViewModels;
using DiplomaGroomingSalon.Service.Interfaces;

namespace DiplomaGroomingSalon.Service.Implementations
{
	public class OrderService : IOrderService
	{
		private readonly IBaseRepository<Order> _orderRepository;
		private readonly IBaseRepository<Appointment> _appointmentRepository;
		private readonly IAccountRepository<Profile> _profileRepository;

		public OrderService(IBaseRepository<Order> orderRepository, IBaseRepository<Appointment> appointmentRepository, IAccountRepository<Profile> profileRepository)
		{
			_orderRepository = orderRepository;
			_appointmentRepository = appointmentRepository;
			_profileRepository = profileRepository;
		}
		public async Task<IBaseResponse<List<Order>>> GetOrders()
		{
			var baseResponse = new BaseResponse<IEnumerable<Order>>();
			try
			{
				var orders = await _orderRepository.GetAll();
				if (!orders.Any())
				{
					return new BaseResponse<List<Order>>()
					{
						Description = "Found 0 items",
						StatusCode = StatusCode.NotFound
					};
				}
				return new BaseResponse<List<Order>>()
				{
					Data = orders.ToList(),
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
                var profile = await _profileRepository.GetByNameAsync(userName);
                var profileView = new ProfileViewModel()
                {
                    Id = profile!.Id,
                    Name = profile.Name,
                    Surname = profile.Surname,
                    Phone = profile.Phone,
                    Email = profile.Email,
                    UserId = profile.UserId
                };

                return new BaseResponse<ProfileViewModel>()
                {
                    Data = profileView,
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
					TypePetId = orderViewModel.PetTypeId,
					BreedPetId = orderViewModel.BreedId,
					ServiceTypeId = orderViewModel.ServiceTypeId,
					Price = orderViewModel.Price
				};
                var appointmentData = await _appointmentRepository.GetAll();

                var appointmentRepository = appointmentData.FirstOrDefault(x => x.Id == orderViewModel.AppointmentId);
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

        public async Task<IBaseResponse<Order>> Edit(Guid id, Order model)
        {
	        try
	        {
		        var orderAsync = await _orderRepository.GetByIdAsync(id);
		        if (orderAsync == null)
			        return new BaseResponse<Order>
			        {
				        Description = "Not found",
				        StatusCode = StatusCode.NotFound
			        };
		        orderAsync.Id = model.Id;
				orderAsync.TypePetId = model.TypePetId;
				orderAsync.BreedPetId = model.BreedPetId;
				orderAsync.ServiceTypeId = model.ServiceTypeId;
				orderAsync.AppointmentId = model.AppointmentId;
				orderAsync.ProfileId = model.ProfileId;
		        orderAsync.StatusOrder  = model.StatusOrder;
				orderAsync.Price = model.Price;
				orderAsync.NamePet = model.NamePet;
				orderAsync.Description = model.Description;
				await _orderRepository.Update(orderAsync);

				return new BaseResponse<Order>
		        {
			        Data = orderAsync,
			        StatusCode = StatusCode.OK
		        };
	        }
	        catch (Exception ex)
	        {
		        return new BaseResponse<Order>
		        {
			        Description = $"[EditOrder] : {ex.Message}",
			        StatusCode = StatusCode.InternalServerError
		        };
	        }
        }

        public async Task<IBaseResponse<Order>> GetById(Guid id)
        {
	        try
	        {
		        var orderAsync = await _orderRepository.GetByIdAsync(id);
		        if (orderAsync == null)
			        return new BaseResponse<Order>
			        {
				        Description = "Not Found",
				        StatusCode = StatusCode.NotFound
			        };

		        var data = new Order()
		        {
			        Id = orderAsync.Id,
					Appointment = orderAsync.Appointment,
					AppointmentId = orderAsync.AppointmentId,
					Profile = orderAsync.Profile,
					ProfileId = orderAsync.ProfileId,
					ServiceType = orderAsync.ServiceType,
					ServiceTypeId = orderAsync.ServiceTypeId,
					Price = orderAsync.Price,
					StatusOrder = orderAsync.StatusOrder,
					NamePet = orderAsync.NamePet
		        };

		        return new BaseResponse<Order>
		        {
			        StatusCode = StatusCode.OK,
			        Data = data
		        };
	        }
	        catch (Exception ex)
	        {
		        return new BaseResponse<Order>
		        {
			        Description = $"[GetOrder] : {ex.Message}",
			        StatusCode = StatusCode.InternalServerError
		        };
	        }
        }
	}
}
