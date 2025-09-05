namespace src.Domain.Core.Quiz.Domain.Core.Contracts.Repository
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
