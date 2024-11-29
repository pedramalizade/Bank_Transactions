using Quiz_Maktab.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Maktab.Interface.Service
{
    public interface ICardService
    {
        public Card GetCardByNumber(string cardNumber);
        public string CheckCard(string cardNumber, string password);
        public bool UpdateBalance(Card card, float amount);
        public bool CheckCardBalance(Card card, float amount);

    }
}
