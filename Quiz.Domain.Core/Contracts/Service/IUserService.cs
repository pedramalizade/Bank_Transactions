namespace src.Domain.Core.Quiz.Domain.Core.Contracts.Service
{
    public interface IUserService
    {
        public User Login(string username, string password);
        public bool Register(User user);
        public void ShowCardBalance(int userId);
        public void AddCard(int userId, Card card);
        public void RemoveCard(string cardNumber);
        public int GenerateRandomeCode();
    }
}
