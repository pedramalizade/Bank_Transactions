using Quiz_Maktab.APPDbContext;
using Quiz_Maktab.Entities;
using Quiz_Maktab.Interface.Service;
using Quiz_Maktab.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Maktab.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly CardService _cardService;
        private readonly TransactionRepository _transactionRepository;
        public TransactionService()
        {
            _cardService = new CardService();
            _transactionRepository = new TransactionRepository();
        }

        public string Transfer(string sourceCardNumber, string destinationCardNumber,string password,  float amount)
        {
            var result = _cardService.CheckCard(sourceCardNumber, password);
            if(result != "Check Successful.")
            {
                return result;
            }

            var sourceCard = _cardService.GetCardByNumber(sourceCardNumber);
            var destinationCard = _cardService.GetCardByNumber(destinationCardNumber);
            if(destinationCard == null)
            {
                return "destination card not found.";
            }

            if(!_cardService.CheckCardBalance(sourceCard, amount))
            {
                return "faild.";
            }

            _cardService.UpdateBalance(sourceCard, -amount);
            _cardService.UpdateBalance(destinationCard, amount);

            var transaction = new Transaction
            {
                SourceCardNumber = sourceCard.CardNumber,
                DestinationCardNumber = destinationCard.CardNumber,
                Amount = amount,
                TranceactionTime = DateTime.Now,
                IsSuccessful = true
            };

            _transactionRepository.AddTransaction(transaction);

            return "Transfer successfully.";
        }

        public List<Transaction> GetTransactions(string cardNumber)
        {
            var sourceTransactions = _transactionRepository.GetAll().Where(t => t.SourceCard.CardNumber == cardNumber).ToList();
            var destinationTramsactions = _transactionRepository.GetAll().Where(t => t.DestinationCard.CardNumber == cardNumber).ToList();
            return sourceTransactions.ToList();

        }
    }
}
