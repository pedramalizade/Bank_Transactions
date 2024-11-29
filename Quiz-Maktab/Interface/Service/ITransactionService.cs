using Quiz_Maktab.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Maktab.Interface.Service
{
    public interface ITransactionService
    {
        public bool Transfer(string sourceCardNumber, string destinationCardNumber, float amount);
        public List<Transaction> GetTransactions();
    }
}
