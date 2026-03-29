using UniversitetSystem.Enums;

namespace UniversitetSystem.Domain.Users
{
    public abstract class User
    {
        public int ID { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public Role Role { get; protected set; }

        protected User(int id, string name, string email, string password, Role role)
        {
            ID = id;
            Name = name;
            Email = email;
            Password = password;
            Role = role;
        }
    }
}
