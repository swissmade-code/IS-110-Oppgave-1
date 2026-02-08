using UniversitetSystem.Enums;
using UniversitetSystem.Interfaces;

namespace UniversitetSystem.Models.Users
{
    public class Employee : User, IBorrower
    {
        public EmployeePosition Position { get; set; }
        public Department Department { get; set; }

        public Employee(int employeeID, string name, string email, EmployeePosition position, Department department)
        : base(employeeID, name, email)
        {
            Position = position;
            Department = department;
        }
    }
}