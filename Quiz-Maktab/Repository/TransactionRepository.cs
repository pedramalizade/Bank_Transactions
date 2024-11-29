using Quiz_Maktab.APPDbContext;
using Quiz_Maktab.Entities;
using Quiz_Maktab.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Maktab.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _appDbContext;
        public TransactionRepository()
        {
            _appDbContext = new AppDbContext();
        }

        public void AddTransaction(Transaction transaction)
        {
            _appDbContext.Transactions.Add(transaction);
            _appDbContext.SaveChanges();
        }

        public List<Transaction> GetAll()
        {
            return _appDbContext.Transactions.ToList();
        }


        //public Card DestinationCard(string destinationCardNumber)
        //{
        //    var destinationCard = _appDbContext.Cards.FirstOrDefault(c => c.CardNumber == destinationCardNumber);
        //}

        //public Card SourceCard(string sourceCardNumber)
        //{
        //    var sourceCard = _appDbContext.Cards.FirstOrDefault(c => c.CardNumber == sourceCardNumber);
        //}
    }
}
