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
    public class CardService : ICardService
    {
        private readonly CardRepository _cardRepository;
        public CardService()
        {
            _cardRepository = new CardRepository();
        }
        public string CheckCard(string cardNumber, string password)
        {
            var card = _cardRepository.GetCardByNumber(cardNumber);
            if (card == null)
            {
                return "Card Not Found.";
            }

            if (card.Password != password)
            {
                card.FailedAttempts++;
                if (card.FailedAttempts >= 3)
                {
                    card.IsActive = false;
                    _cardRepository.UpdateCard(card);
                    return "Card Is Blocked";
                }
                _cardRepository.UpdateCard(card);
                return "Incorrect password";
            }
            // card.FailedAttempts = 0;
            _cardRepository.UpdateCard(card);
            return "Check Successful.";
        }

        public Card GetCardByNumber(string cardNumber)
        {
            return _cardRepository.GetCardByNumber(cardNumber);
        }

        public bool UpdateBalance(Card card, float amount)
        {
            if (card == null)
            {
                return false;
            }
            card.Balance = card.Balance + amount;
            _cardRepository.UpdateCard(card);
            return true;
        }

        public bool CheckCardBalance(Card card, float amount)
        {
            return card.Balance >= amount;
        }

        public bool IsCardValid(string cardNumber)
        {
            var card = _cardRepository.GetCardByNumber(cardNumber);
            return card != null && card.IsActive;
        }

        public void DeductBalance(Card card, float amount)
        {
            card.Balance -= amount;
            _cardRepository.UpdateCard(card);
        }

        public void AddBalance(Card card, float amount)
        {
            card.Balance += amount;
            _cardRepository.UpdateCard(card);
        }
    }
}
