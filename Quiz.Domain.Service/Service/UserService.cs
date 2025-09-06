namespace src.Domain.Service.Quiz.Domain.Service.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _appDbContext;
        private string path = @"C:\Users\asus\source\repos\Quiz-Maktab\Quiz-Maktab\Code.txt";

        public UserService(IUserRepository userRepository, AppDbContext appDbContext)
        {
            _userRepository = userRepository;
            _appDbContext = appDbContext;
        }

        public bool AddCard(int userId, Card card)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                return false;

            card.UserId = userId;

            var existingCard = _appDbContext.Cards.FirstOrDefault(c => c.CardNumber == card.CardNumber);
            if (existingCard != null)
                return false;

            _appDbContext.Cards.Add(card);
            _appDbContext.SaveChanges();
            return true;
        }

        public int GenerateRandomeCode()
        {
            Random random = new Random();
            int randomeCode = random.Next(1000, 99999);
            string result = JsonConvert.SerializeObject(randomeCode);
            File.WriteAllText(path, result);
            return randomeCode;
        }

        public User Login(string username, string password)
        {
            var user = _userRepository.GetByusername(username);


            if (user != null)
            {
                if (user.Password == password)
                {
                    InMemoryDb.OnlineUser = user;
                    return user;

                }
                return null;
            }
            else
            {
                return null;
            }
        }

        public bool Register(User user)
        {
            var existingUser = _appDbContext.Users.FirstOrDefault(t => t.Username == user.Username);
            if (existingUser != null)
            {
                return false;
            }

            _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();

            InMemoryDb.OnlineUser = user;

            return true;
        }

        public bool RemoveCard(string cardNumber)
        {
            var card = _appDbContext.Cards.FirstOrDefault(c => c.CardNumber == cardNumber);
            if (card == null)
                return false;

            _appDbContext.Cards.Remove(card);
            _appDbContext.SaveChanges();
            return true;
        }

        public List<Card> ShowCardBalance(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                return new List<Card>();

            return _appDbContext.Cards
                .Where(c => c.UserId == userId)
                .ToList();
        }
    }
}