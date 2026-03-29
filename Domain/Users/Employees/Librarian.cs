using UniversitetSystem.Enums;

namespace UniversitetSystem.Domain.Users.Employees
{
    public class Librarian : Employee
    {
        public Librarian(int id, string name, string email, string password)
            : base(id, name, email, password, EmployeePosition.Librarian, Department.Library, Role.Librarian)
        {
        }
    }
}