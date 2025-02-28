using Microsoft.AspNetCore.TestHost;
using NuGet.ContentModel;
using WordFlowServer;
using WordFlowServer.Models;

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
            var response = await _client.GetAsync("/api/Cards/Random");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetRandomTask_ShouldReturnCard()
        {
            // Arrange: Dodaj kilka kart do bazy danych
            var card1 = new Card() { Title = "", Question = "First", Answer = "Answer" };
            var card2 = new Card() { Title = "", Question = "Second", Answer = "Answer" };

            _context.Card.AddRange(card1, card2);
            await _context.SaveChangesAsync();

            // Act: Pobierz losową kartę
            var response = await _client.GetFromJsonAsync<Card>("/api/Cards/Random");

            // Assert
            Assert.NotNull(response);
            Assert.Contains(response.Id, new[] { card1.Id, card2.Id });
        }
    }
}
