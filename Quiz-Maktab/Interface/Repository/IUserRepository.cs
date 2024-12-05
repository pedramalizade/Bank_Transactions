using Quiz_Maktab.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Maktab.Interface.Repository
{
    public interface IUserRepository
    {
        public bool Create(User user);
        public User GetByusername(string username);
        public User GetById(int userId);
        public bool Delete(int id);
        public List<User> GetAll();
        public void ShowCardBalance(int userId);
    }
}
