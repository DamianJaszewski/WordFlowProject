namespace WordFlowServer.Models
{
    public class Card
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        public Categorie? Categorie { get; set; }
    }
}
