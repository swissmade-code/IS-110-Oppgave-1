using UniversitetSystem.Data;
using UniversitetSystem.Interfaces;
using UniversitetSystem.Domain.Users;

namespace UniversitetSystem.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        public void AddUser(User user)
        {
            Database.Users.Add(user);
        }

        public bool ExistsById(int userId)
        {
            return Database.Users.Any(u => u.ID == userId);
        }

        public bool ExistsByEmail(string userEmail)
        {
            return Database.Users.Any(u => u.Email == userEmail);
        }

        public List<User> GetAll()
        {
            return Database.Users.ToList();
        }

        public User? GetByEmail(string email)
        {
            return Database.Users.FirstOrDefault(u => u.Email == email);
        }

        public User? GetById(int userId)
        {
            return Database.Users.FirstOrDefault(u => u.ID == userId);
        }


        public int GetNextUserId()
        {
            return Database.NextUserId();
        }
    }
}
