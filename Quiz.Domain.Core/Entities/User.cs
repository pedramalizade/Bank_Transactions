namespace src.Domain.Core.Quiz.Domain.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public List<Card> Cards { get; set; } = new();
    }
}
