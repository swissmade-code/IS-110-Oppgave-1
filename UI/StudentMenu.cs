using UniversitetSystem.Domain.Students;
using UniversitetSystem.Helpers;
using UniversitetSystem.Interfaces;
using static UniversitetSystem.Helpers.ConsoleHelper;

namespace UniversitetSystem.UI
{
    public class StudentMenu
    {
        private readonly Student _student;
        private readonly ICourseService _courseService;
        private readonly ILibraryService _libraryService;
        private readonly ILoanService _loanService;

        public StudentMenu(
            Student student,
            ICourseService courseService,
            ILibraryService libraryService,
            ILoanService loanService)
        {
            _student = student;
            _courseService = courseService;
            _libraryService = libraryService;
            _loanService = loanService;
        }

        public void Run()
        {
            while (true)
            {
                PrintHeader($"Student: {_student.Name}");

                Console.WriteLine("[1] Enroll in course");
                Console.WriteLine("[2] Unenroll from course");
                Console.WriteLine("[3] View my courses");
                Console.WriteLine("[4] View my grades");
                Console.WriteLine("[5] Search courses");
                Console.WriteLine("[6] Search books");
                Console.WriteLine("[7] Borrow book");
                Console.WriteLine("[8] Return book");
                Console.WriteLine("[0] Logout");

                string? input = Prompt("\nSelect");

                switch (input)
                {
                    case "1": EnrollInCourse(); break;
                    case "2": UnenrollFromCourse(); break;
                    case "3": ShowMyCourses(); break;
                    case "4": ShowMyGrades(); break;
                    case "5": LibraryHelper.SearchCourse(_courseService); break;
                    case "6": LibraryHelper.SearchBook(_libraryService); break;
                    case "7": LibraryHelper.BorrowBook(_loanService, _student.ID); break;
                    case "8": LibraryHelper.ReturnBook(_loanService, _student.ID); break;
                    case "0": return;
                    default:
                        PrintError("Invalid choice.");
                        Pause();
                        break;
                }
            }
        }

        private void EnrollInCourse()
        {
            PrintHeader("Enroll in Course");

            string? code = Prompt("Course code");

            if (string.IsNullOrWhiteSpace(code))
            {
                PrintError("Course code cannot be empty.");
                Pause();
                return;
            }

            var result = _courseService.Enroll(_student.ID, code);

            if (!result.Success)
                PrintError(result.Error!);
            else
                Console.WriteLine($"\nYou are now enrolled in course {code}.");

            Pause();
        }

        private void UnenrollFromCourse()
        {
            PrintHeader("Unenroll from Course");

            string? code = Prompt("Course code");

            if (string.IsNullOrWhiteSpace(code))
            {
                PrintError("Course code cannot be empty.");
                Pause();
                return;
            }

            var result = _courseService.UnEnroll(_student.ID, code);

            if (!result.Success)
                PrintError(result.Error!);
            else
                Console.WriteLine($"\nYou have been unenrolled from course {code}.");

            Pause();
        }

        private void ShowMyCourses()
        {
            PrintHeader("My Courses");

            var result = _courseService.GetStudentCourses(_student.ID);

            if (!result.Success || result.Value.Count == 0)
            {
                Console.WriteLine("You are not enrolled in any courses.");
                Pause();
                return;
            }

            Console.WriteLine($"{"Code",-12} {"Course Name",-30} {"Credits",-12}");
            Console.WriteLine(new string('-', 56));

            foreach (var e in result.Value)
            {
                Console.WriteLine($"{e.Code,-12} {e.Name,-30} {e.Credits,-12}");
            }

            Pause();
        }

        private void ShowMyGrades()
        {
            PrintHeader("My Grades");

            var result = _courseService.GetStudentGrades(_student.ID);

            if (!result.Success)
            {
                PrintError(result.Error!);
                Pause();
                return;
            }

            Console.WriteLine($"{"Code",-12} {"Course Name",-30} {"Grade",-10}");
            Console.WriteLine(new string('-', 54));

            foreach (var (course, grade) in result.Value)
            {
                Console.WriteLine($"{course.Code,-12} {course.Name,-30} {grade,-10}");
            }

            Pause();
        }
    }
}