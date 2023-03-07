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
		public async Task<IBaseResponse<List<Order>>> GetOrdersByAdmin()
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
						StatusCode = StatusCode.OK
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
                    Id = profile.Id,
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
	}
}
