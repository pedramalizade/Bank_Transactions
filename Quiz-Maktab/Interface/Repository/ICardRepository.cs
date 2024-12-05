using Quiz_Maktab.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Maktab.Interface.Repository
{
    public interface ICardRepository
    {
        public Card GetCardByNumber(string cardNumber);
        public void UpdateCard(Card card);
        public bool ChangePassword(string cardNumber, string password, string newPassword);

    }
}
