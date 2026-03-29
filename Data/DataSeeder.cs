using UniversitetSystem.Domain.Users.Employees;
using UniversitetSystem.Domain.Students;
using UniversitetSystem.Domain.Library;
using UniversitetSystem.Enums;
using UniversitetSystem.Interfaces;

namespace UniversitetSystem.Data
{
    public static class DataSeeder
    {
        public static void Seed(
            IUserService userService,
            ICourseService courseService,
            ILibraryService libraryService)
        {
            // ====== STUDENTS ======
            var alice = new Student(userService.GetNextUserId(), "Alice Johnson", "alice@example.com", "alice123");
            var bob = new Student(userService.GetNextUserId(), "Bob Smith", "bob@example.com", "bob123");
            var charlie = new Student(userService.GetNextUserId(), "Charlie Lee", "charlie@example.com", "charlie123");

            userService.AddUser(alice);
            userService.AddUser(bob);
            userService.AddUser(charlie);

            // ====== EXCHANGE STUDENTS ======
            var liam = new ExchangeStudent(
                userService.GetNextUserId(), "Liam Brown", "liam@example.com", "liam123",
                "University of Tokyo", "Japan",
                DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(5)
            );

            var maria = new ExchangeStudent(
                userService.GetNextUserId(), "Maria Garcia", "maria@example.com", "maria123",
                "University of Barcelona", "Spain",
                DateTime.Now.AddMonths(-2), DateTime.Now.AddMonths(4)
            );

            userService.AddUser(liam);
            userService.AddUser(maria);

            // ====== EMPLOYEES ======
            var drEmily = new Teacher(userService.GetNextUserId(), "Dr. Emily Carter", "emily.carter@example.com", "emily123", EmployeePosition.Lecturer, Department.ComputerScience);
            var john = new Employee(userService.GetNextUserId(), "John Wilson", "john.wilson@example.com", "john123", EmployeePosition.LabTechnician, Department.Administration, Role.Staff);
            var sarah = new Employee(userService.GetNextUserId(), "Sarah Brown", "sarah.brown@example.com", "sarah123", EmployeePosition.Administration, Department.Administration, Role.Staff);
            var librarian = new Librarian(userService.GetNextUserId(), "Laura White", "laura.white@example.com", "laura123");

            userService.AddUser(drEmily);
            userService.AddUser(john);
            userService.AddUser(sarah);
            userService.AddUser(librarian);

            // ====== COURSES ======
            var cs101Result = courseService.CreateCourse(
                "CS101",
                "Introduction to Computer Science",
                5,
                30,
                drEmily.ID);

            var math201Result = courseService.CreateCourse(
                "MATH201",
                "Calculus II",
                5,
                25,
                drEmily.ID);

            var phys101Result = courseService.CreateCourse(
                "PHYS101",
                "Physics I",
                4,
                20,
                drEmily.ID);

            var cs101 = cs101Result.Value;
            var math201 = math201Result.Value;
            var phys101 = phys101Result.Value;

            if (cs101 != null)
            {
                courseService.Enroll(alice.ID, cs101.Code);
                courseService.Enroll(bob.ID, cs101.Code);
            }

            if (math201 != null)
            {
                courseService.Enroll(charlie.ID, math201.Code);
            }

            if (phys101 != null)
            {
                courseService.Enroll(liam.ID, phys101.Code);
                courseService.Enroll(maria.ID, phys101.Code);
            }

            // ====== LIBRARY ITEMS ======
            libraryService.AddLibraryItem(new LibraryItem(
                libraryService.GetNextLibraryItemId(),
                "C# in Depth",
                "Jon Skeet",
                2019,
                5,
                MediaType.Book));

            libraryService.AddLibraryItem(new LibraryItem(
                libraryService.GetNextLibraryItemId(),
                "Physics Fundamentals",
                "David Halliday",
                2018,
                3,
                MediaType.Book));

            libraryService.AddLibraryItem(new LibraryItem(
                libraryService.GetNextLibraryItemId(),
                "Calculus Workbook",
                "James Stewart",
                2020,
                4,
                MediaType.Book));

            libraryService.AddLibraryItem(new LibraryItem(
                libraryService.GetNextLibraryItemId(),
                "Introduction to Algorithms",
                "Cormen et al.",
                2017,
                2,
                MediaType.Book));

            libraryService.AddLibraryItem(new LibraryItem(
                libraryService.GetNextLibraryItemId(),
                "Computer Science Basics DVD",
                "Jane Doe",
                2021,
                1,
                MediaType.DVD));
        }
    }
}
