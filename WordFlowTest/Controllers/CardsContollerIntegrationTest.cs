using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using NuGet.Versioning;
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
        public async Task GetRandomCard_ReturnOk()
        {
            //Arrange
            var card = new Card() { Title = "Test1", Question = "First", Answer = "Answer" };

            _context.Card.Add(card);
            _context.SaveChanges();

            //Act
            var response = await _client.GetAsync("/api/Cards/Random");

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetRandomCard_ReturnNoContent_WhereThereIsNoCard()
        {
            //Act
            var response = await _client.GetAsync("/api/Cards/Random");

            //Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetRandomCard_ShouldReturnCard()
        {
            // Arrange: Dodaj kilka kart do bazy danych
            var card1 = new Card() { Title = "Test1", Question = "First", Answer = "Answer" };
            var card2 = new Card() { Title = "", Question = "Second", Answer = "Answer" };

            _context.Card.AddRange(card1, card2);
            await _context.SaveChangesAsync();

            // Act: Pobierz losową kartę
            var response = await _client.GetFromJsonAsync<Card>("/api/Cards/Random");

            // Assert
            Assert.NotNull(response);
            Assert.Contains(response.Id, new[] { card1.Id, card2.Id });
        }

        [Fact]
        public async Task PostCard_ReturnOk()
        {
            //Arrange: Tworzenie obiektu 
            var newCard = new Card { Title = "Test", Question = "What is the capital of France?", Answer = "France" };

            //Act
            var response = await _client.PostAsJsonAsync("/api/Cards", newCard);

            // Assert: Sprawdzenie, czy odpowiedź jest poprawna (HTTP 201 Created)
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);

            // Odczytanie obiektu z odpowiedzi
            var createdCard = await response.Content.ReadFromJsonAsync<Card>();
            Assert.NotNull(createdCard);
            Assert.Equal(newCard.Title, createdCard.Title);

            // Sprawdzenie, czy obiekt został zapisany do bazy danych
            var cardInDb = _context.Card.FirstOrDefault(c => c.Id == createdCard.Id);
            Assert.NotNull(cardInDb);
            Assert.Equal("Test", cardInDb.Title);
        }

        [Fact]
        public async Task RepetitionCard_ReturnOk()
        {
            //Arrange: Tworzenie obiektu 
            var newCard = new Card { Title = "Test", Question = "What is the capital of France?", Answer = "France" };

            _context.Card.Add(newCard);
            _context.SaveChanges();

            var response = await _client.PutAsync($"/api/Cards/Repeat?id={newCard.Id}&days=3", null);

            //Assert
            response.EnsureSuccessStatusCode();
        }


        [Fact]
        public async Task RepetitionCard_ShouldCreateRepetition()
        {
            //Arrange: Tworzenie obiektu 
            var newCard = new Card { Title = "Test", Question = "What is the capital of France?", Answer = "France" };

            _context.Card.Add(newCard);
            _context.SaveChanges();

            var response = await _client.PutAsync($"/api/Cards/Repeat?id={newCard.Id}&days=3", null);

            //Assert
            response.EnsureSuccessStatusCode();

            var repetition = await _context.Repetitions.FirstOrDefaultAsync(r => r.CardId == newCard.Id);
            Assert.NotNull(repetition);
            Assert.Equal(3, repetition.Days);

        }
    }
}
