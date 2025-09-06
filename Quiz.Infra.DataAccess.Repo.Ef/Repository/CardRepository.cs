namespace src.Infrastructure.DataAccess.Quiz.Infra.DataAccess.Repo.Ef.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _appDbContext;
        public CardRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Card GetCardByNumber(string cardNumber)
        {
            return _appDbContext.Cards.FirstOrDefault(c => c.CardNumber == cardNumber);
        }
        public bool ChangePassword(string cardNumber, string password, string newPassword)
        {
            var card = GetCardByNumber(cardNumber);
            if (card == null)  
            {
                Console.WriteLine("UserName not found.");
                return false;
            }
            if (card.Password != password)
            {
                Console.WriteLine("Password is Wrong");
                return false;
            }
            card.Password = newPassword;
            _appDbContext.SaveChanges();
            Console.WriteLine("change password is success.");
            return true;
        }

        public void UpdateCard(Card card)
        {
            _appDbContext.Cards.Update(card);
            _appDbContext.SaveChanges();
        }
    }
}
