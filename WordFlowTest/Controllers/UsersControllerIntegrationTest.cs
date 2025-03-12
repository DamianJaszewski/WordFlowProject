using Microsoft.OpenApi.Models;
using WordFlowServer;
using WordFlowServer.Models;

namespace WordFlowTest.Controllers
{
    public class UsersControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        private readonly DataContext _context;
        private readonly IServiceScope _scope;

        public UsersControllerIntegrationTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _scope = _factory.Services.CreateScope();
            _context = _scope.ServiceProvider.GetRequiredService<DataContext>();
            _context.Database.EnsureCreated();
            _client = factory.CreateClient();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _scope.Dispose();
        }

        [Fact]
        public async Task RegisterUser_ReturnOk()
        {
            //Arrange: Tworzenie obiektu 
            var userDto = new UserDto()
            {
                Email = "someaddress@fistaszki.com",
                Password = "zaq1!QAZ",
                PhoneNumber = "1234567890",
                UserName = "username"
            };

            //Act
            var response = await _client.PostAsJsonAsync("/api/Users/Register", userDto);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task RegisterUser_WithBadPassword_ReturnBadRequest()
        {
            //Arrange: Tworzenie obiektu 
            var userDto = new UserDto()
            {
                Email = "someaddress@fistaszki.com",
                Password = "password",
                PhoneNumber = "1234567890",
                UserName = "username"
            };

            //Act
            var response = await _client.PostAsJsonAsync("/api/Users/Register", userDto);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
