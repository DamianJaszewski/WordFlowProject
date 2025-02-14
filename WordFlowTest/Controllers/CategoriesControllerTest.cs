using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using WordFlowServer;

namespace WordFlowTest.Controllers
{
    public class CategoriesControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly DataContext _context;

        public CategoriesControllerTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateDefaultClient();

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(
                "Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=WordFlowTests");
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();

            var _options = optionsBuilder.Options;
            _context = new DataContext(_options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetAll_EndpointsReturnSuccess()
        {
            var response = await _client.GetAsync("/Categories");
            response.EnsureSuccessStatusCode();
        }
    }
}
