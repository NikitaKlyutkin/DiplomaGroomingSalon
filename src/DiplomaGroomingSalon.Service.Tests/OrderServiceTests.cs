using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Service.Implementations;
using Moq;

namespace DiplomaGroomingSalon.Service.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task GetOrdersByAdmin_ShouldGetAllOrders()
        {
            // Mock, Fake, Stub

            // arrange
            var orderService = new OrderService(
                Mock.Of<IBaseRepository<Order>>(),
                Mock.Of<IBaseRepository<Appointment>>(),
                Mock.Of<IAccountRepository<Profile>>()); // sub (subject), sut (sytem under test)

            // act
            _ = await orderService.GetOrdersByAdmin();

            // assert
        }
    }
}