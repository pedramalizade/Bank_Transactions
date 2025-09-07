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
                message = "سقف انتقال پر شده است.";
                return false;
            }

            if (transactionamount + amount > 250)
            {
                message = $"سقف انتقال پر شده مبلغ وارد شده زیاد است {250 - transactionamount}";
                return false;
            }

            if (amount <= 0)
            {
                message = "مبلغ انتقال نمیتونه 0 باشد.";
                return false;
            }

            var sourceCard = _cardService.GetCardByNumber(sourceCardNumber);
            var destinationCard = _cardService.GetCardByNumber(destinationCardNumber);

            if (sourceCard == null || destinationCard == null)
            {
                message = "کارت فرستنده یا گیرنده پیدا نشد.";
                return false;
            }

            if (!_cardService.CheckCardBalance(sourceCard, amount))
            {
                message = "موجودی کارت مبدأ کافی نیست.";
                return false;
            }

            var result = _cardService.ReduceAmount(amount, sourceCardNumber, destinationCardNumber);
            if (!result)
            {
                message = "عملیات انتقال در حین پردازش با خطا مواجه شد.";
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
            message = "انتقال با موفقیت انجام شد.";
            return true;
        }

        public List<Transaction> GetTransactions(string cardNumber)
        {
            return _transactionRepository.GetAllTransaction(cardNumber);
        }
    }
}
