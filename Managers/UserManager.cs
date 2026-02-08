using UniversitetSystem.Models;

namespace UniversitetSystem.Managers
{
    public static class UserManager
    {
        private static List<User> _users = new List<User>();

        public static bool AddUser(User user)
        {
            if (_users.Any(u => u.ID == user.ID))
            {
                return false;
            }

            _users.Add(user);

            return true;
        }

        public static User? SelectUser()
        {
            if (!_users.Any())
            {
                Console.WriteLine("No users registered.");
                return null;
            }

            Console.WriteLine("Select a user:");
            for (int i = 0; i < _users.Count; i++)
            {
                var u = _users[i];
                Console.WriteLine($"[{i + 1}] {u.Name} (ID: {u.ID})");
            }

            Console.Write("Your choice: ");
            if (int.TryParse(Console.ReadLine(), out int choice) &&
                choice > 0 && choice <= _users.Count)
            {
                return _users[choice - 1];
            }

            Console.WriteLine("Invalid choice.");
            return null;
        }

        public static Student? SelectStudent()
        {
            var students = _users.OfType<Student>().ToList();

            if (!students.Any())
            {
                Console.WriteLine("No students registered.");
                return null;
            }

            Console.WriteLine("Select a student:");
            for (int i = 0; i < students.Count; i++)
            {
                var s = students[i];
                Console.WriteLine($"[{i + 1}] {s.Name} (ID: {s.ID})");
            }

            Console.Write("Your choice: ");
            if (int.TryParse(Console.ReadLine(), out int choice) &&
                choice > 0 && choice <= students.Count)
            {
                return students[choice - 1];
            }

            Console.WriteLine("Invalid choice.");
            return null;
        }

    }
}
