using Microsoft.AspNetCore.TestHost;
using WordFlowServer;

namespace WordFlowTest.Controllers
{
    public class CardsContollerIntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        private readonly DataContext _context;
        private readonly IServiceScope _scope;

        public CardsContollerIntegrationTest(CustomWebApplicationFactory<Program> factory)
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
        public async Task GetRandomTask_ReturnOk()
        {
            var response = await _client.GetAsync("/tag/populate");
        }
    }
}
