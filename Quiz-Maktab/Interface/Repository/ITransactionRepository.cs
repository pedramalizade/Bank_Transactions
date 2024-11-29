using Quiz_Maktab.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Maktab.Interface.Repository
{
    public interface ITransactionRepository
    {
        //public Card SourceCard(string sourceCardNumber);
        //public Card DestinationCard(string destinationCardNumber);

        public void AddTransaction(Transaction transaction);
        public List<Transaction> GetAll();

    }
}
