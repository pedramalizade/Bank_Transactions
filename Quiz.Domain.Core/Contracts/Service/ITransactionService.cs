namespace src.Domain.Core.Quiz.Domain.Core.Contracts.Service
{
    public interface ITransactionService
    {
        public bool Transfer(string sourceCardNumber, string destinationCardNumber, float amount);
        public List<Transaction> GetTransactions(string cardNumber);
    }
}
