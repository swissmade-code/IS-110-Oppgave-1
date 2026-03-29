using UniversitetSystem.Domain.Users;

namespace UniversitetSystem.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User? GetByEmail(string email);
        User? GetById(int userId);
        bool ExistsById(int userId);
        bool ExistsByEmail(string userEmail);
        int GetNextUserId();
        List<User> GetAll();
    }
}