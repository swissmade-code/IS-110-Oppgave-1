using UniversitetSystem.Enums;
using UniversitetSystem.Domain.Courses;

namespace UniversitetSystem.Domain.Users.Employees
{
    public class Teacher : Employee
    {
        public Teacher(int id, string name, string email, string password,
                      EmployeePosition position, Department department)
           : base(id, name, email, password, position, department, Role.Teacher)
        {
        }
    }
}
