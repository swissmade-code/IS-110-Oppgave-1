using UniversitetSystem.Domain.Users;
using UniversitetSystem.Interfaces;

namespace UniversitetSystem.Tests.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new();

        public void AddUser(User user) => _users.Add(user);

        public bool ExistsByEmail(string userEmail) => _users.Any(u => u.Email == userEmail);

        public bool ExistsById(int userId) => _users.Any(u => u.ID == userId);

        public List<User> GetAll() => _users;

        public User? GetByEmail(string email) => _users.FirstOrDefault(u => u.Email == email);

        public User? GetById(int userId) => _users.FirstOrDefault(u => u.ID == userId);

        public int GetNextUserId() => _users.Count == 0 ? 1 : _users.Max(u => u.ID) + 1;
    }
}