using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using System.Net.WebSockets;
using WordFlowServer.Models;

namespace WordFlowServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly DataContext _context;

        public CardsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetCards()
        {
            return await _context.Card.ToListAsync();
        }

        // GET: api/Cards/Random
        [HttpGet("Random")]
        public async Task<ActionResult<Card>> GetRandomCard()
        {
            var cards = await _context.Card.ToListAsync();

            if (!cards.Any())
            {
                return NoContent(); // 204 No Content
            }

            var random = new Random();
            var output = cards.ElementAt(random.Next(cards.Count));

            return output;
        }

        // Put: api/Cards/Repeat
        [HttpPut("Repeat")]
        public async Task<ActionResult> RepeatCard(int id, int days)
        {
            var card = await _context.Card.FindAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            var repetition = new Repetition
            {
                CardId = card.Id,
                UpdateDate = DateTime.UtcNow,
                NextRepetitionDate = DateTime.UtcNow.AddDays(days),
                Days = days
            };

            card.Repetitions.Add(repetition);
            _context.SaveChanges();

            return Ok();
        }

        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCard(int id)
        {
            var card = await _context.Card.FindAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        // PUT: api/Cards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(int id, Card card)
        {
            if (id != card.Id)
            {
                return BadRequest();
            }

            _context.Entry(card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(Card card)
        {
            _context.Card.Add(card);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCard", new { id = card.Id }, card);
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var card = await _context.Card.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }

            _context.Card.Remove(card);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardExists(int id)
        {
            return _context.Card.Any(e => e.Id == id);
        }
    }
}
