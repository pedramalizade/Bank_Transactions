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
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _appDbContext;
        public CardRepository()
        {
            _appDbContext = new AppDbContext();
        }
        public Card GetCardByNumber(string cardNumber)
        {
            return _appDbContext.Cards.FirstOrDefault(c => c.CardNumber == cardNumber);
        }

        public void UpdateCard(Card card)
        {
            _appDbContext.Cards.Update(card);
            _appDbContext.SaveChanges();
        }
    }
}
