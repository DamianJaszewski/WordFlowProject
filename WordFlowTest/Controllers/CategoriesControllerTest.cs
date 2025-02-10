using Microsoft.AspNetCore.Mvc.Testing;
using WordFlowServer;

namespace WordFlowTest.Controllers
{
    public class CategoriesControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CategoriesControllerTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateDefaultClient();    
        }

        [Fact]
        public async Task GetAll_EndpointsReturnSuccess()
        {
            var response = await _client.GetAsync("/Categories");
            response.EnsureSuccessStatusCode();
        }
    }
}
