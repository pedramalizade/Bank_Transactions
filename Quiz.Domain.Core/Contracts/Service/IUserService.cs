namespace src.Domain.Core.Quiz.Domain.Core.Contracts.Service
{
    public interface IUserService
    {
        public User Login(string username, string password);
        public bool Register(User user);
        public List<Card> ShowCardBalance(int userId);
        public bool AddCard(int userId, Card card);
        public bool RemoveCard(string cardNumber);
        public int GenerateRandomeCode();
    }
}
