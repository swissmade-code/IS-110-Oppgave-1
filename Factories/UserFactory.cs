using UniversitetSystem.Enums;
using UniversitetSystem.Domain.Students;
using UniversitetSystem.Domain.Users;
using UniversitetSystem.Domain.Users.Employees;

namespace UniversitetSystem.Factories
{
    public static class UserFactory
    {
        public static User CreateUser(Role role, int id, string name, string email, string password, Department? department)
        {
            return role switch
            {
                Role.Student => new Student(id, name, email, password),
                Role.Teacher => new Teacher(id, name, email, password, EmployeePosition.Lecturer, Department.Mathematics),
                Role.Librarian => new Librarian(id, name, email, password),

                _ => throw new ArgumentException("Invalid role")
            };
        }
    }
}