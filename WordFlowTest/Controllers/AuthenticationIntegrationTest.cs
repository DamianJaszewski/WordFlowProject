using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using WordFlowServer;
using WordFlowServer.Models;

namespace WordFlowTest.Controllers
{
    public class AuthenticationIntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        private readonly DataContext _context;
        private readonly IServiceScope _scope;
        private readonly UserManager<IdentityUser> _userManager;
        public AuthenticationIntegrationTest(CustomWebApplicationFactory<Program> factory, UserManager<IdentityUser> userManager)
        {
            _factory = factory;
            _scope = _factory.Services.CreateScope();
            _context = _scope.ServiceProvider.GetRequiredService<DataContext>();
            _context.Database.EnsureCreated();
            _client = factory.CreateClient();
            _userManager = userManager;
        }
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _scope.Dispose();
        }

        [Fact]
        public async Task Register_ReturnOk()
        {
            var newUser = new RegisterRequest()
            {
                Email = "someaddress@fistaszki.com",
                Password = "zaq1!QAZ"
            };

            // Act: Pobierz losową kartę
            var response = await _client.PostAsJsonAsync("/register", newUser);

            //Assert
            response.EnsureSuccessStatusCode();
        }  
        
        [Fact]
        public async Task Login_ReturnOk()
        {
            //Arrange
            var user = new IdentityUser
            {
                UserName = "UserName",
                Email = "someaddress@fistaszki.com",
            };

            var password = "zaq1!QAZ";
            await _userManager.CreateAsync(user, password);

            var loginRequest = new LoginRequest()
            {
                Email = "someaddress@fistaszki.com",
                Password = password
            };

            // Act: Pobierz losową kartę
            var response = await _client.PostAsJsonAsync("/login", loginRequest);

            //Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
