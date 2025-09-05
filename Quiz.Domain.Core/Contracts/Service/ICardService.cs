namespace src.Domain.Core.Quiz.Domain.Core.Contracts.Service
{
    public interface ICardService
    {
        public Card GetCardByNumber(string cardNumber);
        public string CheckCard(string cardNumber, string password);
        public bool UpdateBalance(Card card, float amount);
        public bool CheckCardBalance(Card card, float amount);
        public bool IsCardValid(string cardNumber);
        public void DeductBalance(Card card, float amount);
        public void AddBalance(Card card, float amount);
        public bool ChangePassword(string cardNumber, string password, string newPassword);
        public bool ReduceAmount(double money, string cardNumber, string distanceCardNumber);
        public bool GetHolderNameCard(string cardNumber);
    }
}
