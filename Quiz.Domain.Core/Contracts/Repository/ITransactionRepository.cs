namespace src.Domain.Core.Quiz.Domain.Core.Contracts.Repository
{
    public interface ITransactionRepository
    {
        public void AddTransaction(Transaction transaction);
        public List<Transaction> GetAllTransaction(string cardNumber);
        public float TransactionAmountInDay(string cardnumber);
    }
}
