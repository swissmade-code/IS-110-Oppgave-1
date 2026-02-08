

namespace UniversitetSystem.Models
{
    public abstract class User
    {
        public int ID { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }

        protected User(int id, string name, string email)
        {
            ID = id;
            Name = name;
            Email = email;
        }
    }
}