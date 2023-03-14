using DiplomaGroomingSalon.DAL.Interfaces;
using DiplomaGroomingSalon.Domain.Entities;
using DiplomaGroomingSalon.Domain.Enum;
using DiplomaGroomingSalon.Domain.Response;
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
            var orderRepositoryMock = new Mock<IBaseRepository<Order>>();
            orderRepositoryMock.Setup(repo => repo.GetAll())
                .ReturnsAsync(new[] { new Order() });
            var orderService = new OrderService(
                orderRepositoryMock.Object,
                Mock.Of<IBaseRepository<Appointment>>(),
                Mock.Of<IAccountRepository<Profile>>()); // sub (subject), sut (sytem under test)

            // act
            var result = await orderService.GetOrdersByAdmin();

            // assert
            orderRepositoryMock.Verify(repo => repo.GetAll(), Times.AtLeastOnce());
            Assert.Equal(StatusCode.OK, result.StatusCode);
        }
    }
}