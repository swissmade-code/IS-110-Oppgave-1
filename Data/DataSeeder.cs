using UniversitetSystem.Enums;
using UniversitetSystem.Managers;
using UniversitetSystem.Models;
using UniversitetSystem.Models.Courses;
using UniversitetSystem.Models.Library;
using UniversitetSystem.Models.Users;

namespace UniversitetSystem.Data
{
    public class DataSeeder
    {
        public static void Seed()
        {
            // Students
            UserManager.AddUser(new Student(1, "Alice Johnson", "alice@example.com"));
            UserManager.AddUser(new Student(2, "Bob Smith", "bob@example.com"));
            UserManager.AddUser(new Student(3, "Charlie Lee", "charlie@example.com"));

            // Exchange Students
            UserManager.AddUser(new ExchangeStudent(
                4, "Liam Brown", "liam@example.com",
                "University of Tokyo", "Japan",
                DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(5)
            ));
            UserManager.AddUser(new ExchangeStudent(
                5, "Maria Garcia", "maria@example.com",
                "University of Barcelona", "Spain",
                DateTime.Now.AddMonths(-2), DateTime.Now.AddMonths(4)
            ));

            // Employees
            UserManager.AddUser(new Employee(6, "Dr. Emily Carter", "emily.carter@example.com", EmployeePosition.Lecturer, Department.ComputerScience));
            UserManager.AddUser(new Employee(7, "John Wilson", "john.wilson@example.com", EmployeePosition.LabTechnician, Department.Administration));
            UserManager.AddUser(new Employee(8, "Sarah Brown", "sarah.brown@example.com", EmployeePosition.Administration, Department.Administration));

            // Courses
            CourseManager.AddCourse(new Course("CS101", "Introduction to Computer Science", 5, 30));
            CourseManager.AddCourse(new Course("MATH201", "Calculus II", 5, 25));
            CourseManager.AddCourse(new Course("PHYS101", "Physics I", 4, 20));

            // Library Items
            LibraryManager.AddLibraryItem(new LibraryItem(1, "C# in Depth", "Jon Skeet", 2019, 5, MediaType.Book));
            LibraryManager.AddLibraryItem(new LibraryItem(2, "Physics Fundamentals", "David Halliday", 2018, 3, MediaType.Book));
            LibraryManager.AddLibraryItem(new LibraryItem(3, "Calculus Workbook", "James Stewart", 2020, 4, MediaType.Book));
            LibraryManager.AddLibraryItem(new LibraryItem(4, "Introduction to Algorithms", "Cormen et al.", 2017, 2, MediaType.Book));
            LibraryManager.AddLibraryItem(new LibraryItem(5, "Computer Science Basics DVD", "Jane Doe", 2021, 1, MediaType.DVD));
        }
    }
}