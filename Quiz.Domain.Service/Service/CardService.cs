namespace src.Domain.Service.Quiz.Domain.Service.Service
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public string CheckCard(string cardNumber, string password)
        {
            var card = _cardRepository.GetCardByNumber(cardNumber);
            if (card == null)
            {
                return "کارت پیدا نشد.";
            }

            if (card.Password != password)
            {
                card.FailedAttempts++;
                if (card.FailedAttempts >= 3)
                {
                    card.IsActive = false;
                    _cardRepository.UpdateCard(card);
                    return "کارت مسدود است";
                }
                _cardRepository.UpdateCard(card);
                return "رمز نادرست است";
            }
            card.FailedAttempts = 0;
            _cardRepository.UpdateCard(card);
            return "درست است!.";
        }
        public Card GetCardByNumber(string cardNumber)
        {
            return _cardRepository.GetCardByNumber(cardNumber);
        }
        public bool ChangePassword(string cardNumber, string password, string newPassword)
        {
            var card = _cardRepository.GetCardByNumber(cardNumber);
            if (card == null || card.Password != password)
                return false;

            card.Password = newPassword;
            _cardRepository.UpdateCard(card);
            return true;
        }
        public bool UpdateBalance(string cardNumber, float amount)
        {
            var card = _cardRepository.GetCardByNumber(cardNumber);
            if (card == null || !card.IsActive)
                return false;

            card.Balance += amount;
            _cardRepository.UpdateCard(card);
            return true;
        }
        public bool GetHolderNameCard(string cardNumber, out string holderName)
        {
            var card = _cardRepository.GetCardByNumber(cardNumber);
            holderName = card?.HolderName;

            if (string.IsNullOrEmpty(holderName))
                return false;

            return true;
        }
        public bool CheckCardBalance(Card card, float amount)
        {
            return card != null && card.Balance >= amount;
        }
        public bool IsCardValid(string cardNumber)
        {
            if (cardNumber.Length != 16)
                return false;

            var card = _cardRepository.GetCardByNumber(cardNumber);
            return card != null && card.IsActive;
        }
        public bool ReduceAmount(double money, string sourceCardNumber, string destinationCardNumber)
        {
            var sourceCard = _cardRepository.GetCardByNumber(sourceCardNumber);
            var destinationCard = _cardRepository.GetCardByNumber(destinationCardNumber);

            if (sourceCard == null || destinationCard == null || money <= 0)
                return false;

            double moneyTax = money > 1000 ? money * 0.015 : money * 0.005;
            double finalAmount = money - moneyTax;

            if (sourceCard.Balance < money)
                return false;

            sourceCard.Balance -= (float)money;
            destinationCard.Balance += (float)finalAmount;

            _cardRepository.UpdateCard(sourceCard);
            _cardRepository.UpdateCard(destinationCard);

            return true;
        }
        public void DeductBalance(Card card, float amount)
        {
            card.Balance -= amount;
            _cardRepository.UpdateCard(card);
        }
        public void AddBalance(Card card, float amount)
        {
            card.Balance += amount;
            _cardRepository.UpdateCard(card);
        }
    }
}
