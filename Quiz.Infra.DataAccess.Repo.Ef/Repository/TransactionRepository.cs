namespace src.Infrastructure.DataAccess.Quiz.Infra.DataAccess.Repo.Ef.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _appDbContext;
        public TransactionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddTransaction(Transaction transaction)
        {
            _appDbContext.Transactions.Add(transaction);
            _appDbContext.SaveChanges();
        }

        public List<Transaction> GetAllTransaction(string cardNumber)
        {
            return _appDbContext.Transactions.Where(t => t.SourceCardNumber == cardNumber || t.DestinationCardNumber == cardNumber).ToList();
        }

        public float TransactionAmountInDay(string cardnumber)
        {
            var transactions = _appDbContext.Transactions.Where(t => t.SourceCardNumber == cardnumber && t.TranceactionTime.DayOfYear == DateTime.Now.DayOfYear).ToList();
            float amount = 0;
            transactions.ForEach(t => amount += t.Amount);
            return amount;
        }
    }
}
