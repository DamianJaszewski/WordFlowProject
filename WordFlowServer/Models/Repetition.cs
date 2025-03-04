namespace WordFlowServer.Models
{
    public class Repetition
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime NextRepetitionDate { get; set; }
        public int Days { get; set; }

        public Card? Card { get; set; }
    }
} 