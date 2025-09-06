namespace src.Domain.Service.Quiz.Domain.Service.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly ICardService _cardService;
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ICardService cardService, ITransactionRepository transactionRepository)
        {
            _cardService = cardService;
            _transactionRepository = transactionRepository;
        }

        public bool Transfer(string sourceCardNumber, string destinationCardNumber, float amount, out string message)
        {
            message = string.Empty;

            var transactionamount = _transactionRepository.TransactionAmountInDay(sourceCardNumber);
            if (transactionamount >= 250)
            {
                message = "Transfer limit has been exceeded.";
                return false;
            }

            if (transactionamount + amount > 250)
            {
                message = $"The transfer limit will be exceeded. Entered amount must be less than {250 - transactionamount}";
                return false;
            }

            if (amount <= 0)
            {
                message = "The transfer amount must be greater than zero.";
                return false;
            }

            var sourceCard = _cardService.GetCardByNumber(sourceCardNumber);
            var destinationCard = _cardService.GetCardByNumber(destinationCardNumber);

            if (sourceCard == null || destinationCard == null)
            {
                message = "Source or destination card not found.";
                return false;
            }

            if (!_cardService.CheckCardBalance(sourceCard, amount))
            {
                message = "Insufficient balance on the source card.";
                return false;
            }

            var result = _cardService.ReduceAmount(amount, sourceCardNumber, destinationCardNumber);
            if (!result)
            {
                message = "Transfer failed during processing.";
                return false;
            }

            var transaction = new Transaction
            {
                SourceCardNumber = sourceCardNumber,
                DestinationCardNumber = destinationCardNumber,
                Amount = amount,
                TranceactionTime = DateTime.Now,
                IsSuccessful = true
            };

            _transactionRepository.AddTransaction(transaction);
            message = "Transfer completed successfully.";
            return true;
        }

        public List<Transaction> GetTransactions(string cardNumber)
        {
            return _transactionRepository.GetAllTransaction(cardNumber);
        }
    }
}
