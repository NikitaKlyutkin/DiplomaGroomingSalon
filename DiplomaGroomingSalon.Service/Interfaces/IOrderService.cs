﻿using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Response;
using DiplomaGroomingSalon.Domain.ViewModels;

namespace DiplomaGroomingSalon.Service.Interfaces;

public interface IOrderService
{
	Task<IBaseResponse<List<Order>>> GetOrders();
	Task<IBaseResponse<OrderViewModel>> CreateOrder(OrderViewModel orderViewModel, Guid ProfileId);
	Task<BaseResponse<ProfileViewModel>> GetProfileOrder(string userName);
	Task<IBaseResponse<Order>> GetById(Guid id);
	Task<IBaseResponse<Order>> Edit(Guid id, Order model);
}