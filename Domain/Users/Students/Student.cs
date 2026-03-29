using UniversitetSystem.Enums;
using UniversitetSystem.Interfaces;
using UniversitetSystem.Domain.Users;

namespace UniversitetSystem.Domain.Students
{
    public class Student : User, IBorrower
    {
        public Student(int studentID, string name, string email, string password) : base(studentID, name, email, password, Role.Student) { }
    }
}
