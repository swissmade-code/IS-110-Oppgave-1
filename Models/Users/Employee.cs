using UniversitetSystem.Enums;
using UniversitetSystem.Interfaces;

namespace UniversitetSystem.Models.Users
{
    public class Employee : User, IBorrower
    {
        public EmployeePosition Position { get; private set; }
        public Department Department { get; private set; }

        public Employee(int employeeID, string name, string email, EmployeePosition position, Department department)
        : base(employeeID, name, email)
        {
            Position = position;
            Department = department;
        }
    }
}