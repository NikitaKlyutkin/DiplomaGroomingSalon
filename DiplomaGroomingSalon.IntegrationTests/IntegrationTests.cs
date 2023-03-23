using DiplomaGroomingSalon.DAL;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Web;

namespace DiplomaGroomingSalon.IntegrationTests
{
    public class IntegrationTests
    {
        [Fact]
        [Trait("Category", "Integration")]
        public async Task RegisterShouldReturnErrorWhenUserExists()
        {
            // arrange
            var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
            optionsBuilder.UseSqlServer("Data Source= localhost; Initial Catalog=DiplomaGroomingSalon;User Id=sa;Password=TMS-NET-07@;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; MultipleActiveResultSets=True");

            using (var context = new DBContext(optionsBuilder.Options))
            {
                await context.Users.AddAsync(new Domain.Entities.User {
                    Id = Guid.NewGuid(),
                    Name = "UserName"
                });
            }

            // act
            var client = new HttpClient();
            var form = new Dictionary<string, string>
            {
                { "Name", "UserName" },
                { "Password", "12345678" },
                { "PasswordConfirm", "12345678" }
            };

            var requestContent = new FormUrlEncodedContent(form);
            var response = await client.PostAsync("http://localhost:5005/account/register", requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            // assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseContent);
            Assert.True(HttpUtility.HtmlDecode(responseContent)?
                .Contains("Пользователь с таким логином уже есть"));
        }
    }
}