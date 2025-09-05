namespace src.Domain.Core.Quiz.Domain.Core.Contracts.Repository
{
    public interface ICardRepository
    {
        public Card GetCardByNumber(string cardNumber);
        public void UpdateCard(Card card);
        public bool ChangePassword(string cardNumber, string password, string newPassword);
    }
}
