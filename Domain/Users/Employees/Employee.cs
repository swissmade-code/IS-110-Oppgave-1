using UniversitetSystem.Enums;
using UniversitetSystem.Interfaces;

namespace UniversitetSystem.Domain.Users.Employees
{
    public class Employee : User, IBorrower
    {
        public EmployeePosition Position { get; private set; }
        public Department Department { get; private set; }

        public Employee(int employeeID, string name, string email, string password, EmployeePosition position, Department department, Role role)
        : base(employeeID, name, email, password, role)
        {
            Position = position;
            Department = department;
        }
    }
}
