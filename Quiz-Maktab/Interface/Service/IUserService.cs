using Quiz_Maktab.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Maktab.Interface.Service
{
    public interface IUserService
    {
        public User Login(string username, string password);
        public bool Register(User user);
        public void ShowCardBalance(int userId);
        public void AddCard(int userId, Card card);
        public void RemoveCard(string cardNumber);
        public int GenerateRandomeCode();
    }
}
