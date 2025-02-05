using Microsoft.Extensions.Hosting;

namespace WordFlowServer.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Card> Cards { get; } = new List<Card>();
    }
}
