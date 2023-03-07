using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Response;
using DiplomaGroomingSalon.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaGroomingSalon.Service.Interfaces
{
	public interface IOrderService
	{
        Task<IBaseResponse<List<Order>>> GetOrdersByAdmin();
		Task<IBaseResponse<OrderViewModel>> CreateOrder(OrderViewModel orderViewModel, Guid ProfileId);
        Task<BaseResponse<ProfileViewModel>> GetProfileOrder(string userName);


    }
}
