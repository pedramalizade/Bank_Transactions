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

        public bool Transfer(string sourceCardNumber, string destinationCardNumber,  float amount)
        {
            var transactionamount = _transactionRepository.TransactionAmountInDay(sourceCardNumber);
            if (transactionamount >= 250)
            {
                Console.WriteLine(" Transfer limit has been exceeded.");
                return false;
            }
            if (transactionamount + amount > 250)
            {
                Console.WriteLine($"The Transfer limit will be  exceeded.Entered amonut must be less than {transactionamount - 250}");
                Console.ReadKey();
                return false;

            }

            if (amount < 0)
            {
                Console.WriteLine("The transfer amount must be greater than zero.");
                Console.ReadKey();
                return false;

            }

            _cardService.ReduceAmount(amount, sourceCardNumber, destinationCardNumber);


            var sourceCard = _cardService.GetCardByNumber(sourceCardNumber);
            var destinationCard = _cardService.GetCardByNumber(destinationCardNumber);

            if(sourceCard == null || destinationCardNumber == null )
            {
                Console.WriteLine("Surce or destination card not found.");
                return false;
            }

            if(!_cardService.CheckCardBalance(sourceCard, amount))
            {
                Console.WriteLine("Insifficient balance on the source card.");
                return false;
            }

            _cardService.DeductBalance(sourceCard, amount);
            _cardService.AddBalance(destinationCard, amount);

            var transaction = new Transaction 
            {
                SourceCardNumber = sourceCardNumber,
                DestinationCardNumber = destinationCardNumber,
                Amount = amount,
                TranceactionTime = DateTime.Now,
                IsSuccessful = true
            };

            _transactionRepository.AddTransaction(transaction);
            return true;

            
        }

        public List<Transaction> GetTransactions(string cardNumber)
        {
            return _transactionRepository.GetAllTransaction(cardNumber);

        }
    }
}
