using Newtonsoft.Json;
using Quiz_Maktab.APPDbContext;
using Quiz_Maktab.Entities;
using Quiz_Maktab.Interface.Repository;
using Quiz_Maktab.Interface.Service;
using Quiz_Maktab.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Maktab.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _appDbContext;
        private string path = @"C:\Users\asus\source\repos\Quiz-Maktab\Quiz-Maktab\Code.txt";

        public UserService()
        {
            _userRepository = new UserRepository();
            _appDbContext = new AppDbContext();
        }

        public void AddCard(int userId, Card card)
        {
            var userid = _userRepository.GetById(userId);
            if (userid != null)
            {
                var user = _appDbContext.Users.Find(userId);
                if (user == null)
                {
                    Console.WriteLine("User does not exist.");
                }
                card.UserId = userId;
                _appDbContext.Cards.Add(card);
                _appDbContext.SaveChanges();
                Console.WriteLine("Card Added.");
            }
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
            var username = _appDbContext.Users.FirstOrDefault(t => t.Username == user.Username && t.Password == user.Password);
            if (username != null)
            {
                return false;
            }
            _appDbContext.Users.Add(user);
            InMemoryDb.OnlineUser = user;
            _appDbContext.SaveChanges();
            return true;
        }

        public void RemoveCard(string cardNumber)
        {
            var remove = _appDbContext.Cards.FirstOrDefault(c => c.CardNumber == cardNumber);
            if (remove != null)
            {
                _appDbContext.Cards.Remove(remove);
                _appDbContext.SaveChanges();
                Console.WriteLine("card item remove successfully.");
            }
            else
            {
                Console.WriteLine("card item not found.");
            }
        }

        public void ShowCardBalance(int userId)
        {
            _userRepository.ShowCardBalance(userId);
        }
    }
}