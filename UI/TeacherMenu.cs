using UniversitetSystem.Domain.Users.Employees;
using UniversitetSystem.Domain.Courses;
using UniversitetSystem.Interfaces;
using static UniversitetSystem.Helpers.ConsoleHelper;
using UniversitetSystem.Helpers;

namespace UniversitetSystem.UI
{
    public class TeacherMenu
    {
        private readonly Teacher _teacher;
        private readonly ICourseService _courseService;
        private readonly ILibraryService _libraryService;
        private readonly ILoanService _loanService;

        public TeacherMenu(
            Teacher teacher,
            ICourseService courseService,
            ILibraryService libraryService,
            ILoanService loanService)
        {
            _teacher = teacher;
            _courseService = courseService;
            _libraryService = libraryService;
            _loanService = loanService;
        }

        public void Run()
        {
            while (true)
            {
                PrintHeader($"Teacher: {_teacher.Name}");

                Console.WriteLine("[1]  Create Course");
                Console.WriteLine("[2]  View My Courses and Students");
                Console.WriteLine("[3]  Assign Grade to Student");
                Console.WriteLine("[4]  Register Course Curriculum");
                Console.WriteLine("[5]  View Course Curriculum");
                Console.WriteLine("[6]  Search Courses");
                Console.WriteLine("[7]  Search Books");
                Console.WriteLine("[8]  Borrow Book");
                Console.WriteLine("[9]  Return Book");
                Console.WriteLine("[0]  Logout");

                string? input = Prompt("\nSelect");

                switch (input)
                {
                    case "1": CreateCourse(); break;
                    case "2": ShowMyCourses(); break;
                    case "3": GradeStudent(); break;
                    case "4": RegisterCurriculum(); break;
                    case "5": ShowCurriculum(); break;
                    case "6": SearchCourse(); break;
                    case "7": LibraryHelper.SearchBook(_libraryService); break;
                    case "8": LibraryHelper.BorrowBook(_loanService, _teacher.ID); break;
                    case "9": LibraryHelper.ReturnBook(_loanService, _teacher.ID); break;
                    case "0": return;
                    default:
                        PrintError("Invalid selection.");
                        Pause();
                        break;
                }
            }
        }

        private void CreateCourse()
        {
            PrintHeader("Create Course");

            string? code = Prompt("Course code (e.g., CS101)");
            string? name = Prompt("Course name");
            int? credits = PromptInt("Credits");
            int? capacity = PromptInt("Maximum students");

            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(name))
            {
                PrintError("Course code and name are required.");
                Pause();
                return;
            }

            if (credits == null || credits <= 0)
            {
                PrintError("Invalid number of credits.");
                Pause();
                return;
            }

            if (capacity == null || capacity <= 0)
            {
                PrintError("Invalid capacity.");
                Pause();
                return;
            }

            var result = _courseService.CreateCourse(code, name, credits.Value, capacity.Value, _teacher.ID);

            if (result.Success)
                Console.WriteLine($"\nCourse '{name}' ({code}) has been created!");
            else
                PrintError(result.Error);

            Pause();
        }

        private void ShowMyCourses()
        {
            PrintHeader("My Courses and Students");

            var courses = _courseService.GetCoursesByTeacher(_teacher.ID);

            if (!courses.Any())
            {
                Console.WriteLine("You are not teaching any courses yet.");
                Pause();
                return;
            }

            foreach (var course in courses)
            {
                Console.WriteLine($"[{course.Code}] {course.Name} - {course.Credits} credits - {course.EnrolledStudents.Count}/{course.MaxStudents} enrolled");

                var enrollments = _courseService.GetEnrollmentsForCourse(course.Code);

                if (!enrollments.Any())
                {
                    Console.WriteLine("   (no students enrolled)\n");
                    continue;
                }

                Console.WriteLine($"   {"StudentID",-12} {"Name",-25} {"Email",-30} {"Grade",-10}");
                Console.WriteLine("   " + new string('-', 79));

                foreach (var e in enrollments)
                {
                    Console.WriteLine($"   {e.Student.ID,-12} {e.Student.Name,-25} {e.Student.Email,-30} {e.Grade ?? "-",-10}");
                }

                Console.WriteLine();
            }

            Pause();
        }

        private void GradeStudent()
        {
            PrintHeader("Assign Grade");

            string? code = Prompt("Course code");

            if (string.IsNullOrWhiteSpace(code))
            {
                PrintError("Course code cannot be empty.");
                Pause();
                return;
            }

            var courses = _courseService.GetCoursesByTeacher(_teacher.ID);
            var course = courses.FirstOrDefault(c => c.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

            if (course == null)
            {
                PrintError("You are not teaching this course.");
                Pause();
                return;
            }

            int? studentId = PromptInt("Student ID");

            if (studentId == null)
            {
                PrintError("Invalid student ID.");
                Pause();
                return;
            }

            if (!course.EnrolledStudents.Any(s => s.ID == studentId))
            {
                PrintError("Student is not enrolled in this course.");
                Pause();
                return;
            }

            if (!course.Curriculum.Any())
            {
                PrintError("No assignments in this course.");
                Pause();
                return;
            }

            Console.WriteLine("\nSelect Assignment to grade:");

            for (int i = 0; i < course.Curriculum.Count; i++)
            {
                Console.WriteLine($"[{i}] {course.Curriculum[i].Name} (Deadline: {course.Curriculum[i].Deadline:yyyy-MM-dd})");
            }

            int? assIndex = PromptInt("Assignment number");

            if (assIndex == null || assIndex < 0 || assIndex >= course.Curriculum.Count)
            {
                PrintError("Invalid assignment selection.");
                Pause();
                return;
            }

            var assignment = course.Curriculum[assIndex.Value];

            string? grade = Prompt("Grade (A, B, C, D, F)")?.ToUpper();

            if (string.IsNullOrWhiteSpace(grade) || !IsValidGrade(grade))
            {
                PrintError("Invalid grade.");
                Pause();
                return;
            }

            var result = _courseService.SetGrade(studentId.Value, code, assignment.Id, grade);

            if (result.Success)
                Console.WriteLine($"\nGrade {grade} assigned to student {studentId} for assignment '{assignment.Name}'.");
            else
                PrintError(result.Error);

            Pause();
        }

        private void RegisterCurriculum()
        {
            PrintHeader("Register Curriculum");

            string? code = Prompt("Course code");

            if (string.IsNullOrWhiteSpace(code))
            {
                PrintError("Course code cannot be empty.");
                Pause();
                return;
            }

            var courses = _courseService.GetCoursesByTeacher(_teacher.ID);
            var course = courses.FirstOrDefault(c => c.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

            if (course == null)
            {
                PrintError("You are not teaching this course.");
                Pause();
                return;
            }

            string? name = Prompt("Assignment name");

            if (string.IsNullOrWhiteSpace(name))
            {
                PrintError("Assignment name cannot be empty.");
                Pause();
                return;
            }

            string? desc = Prompt("Description");

            string? deadlineStr = Prompt("Deadline (yyyy-MM-dd)");

            if (!DateTime.TryParse(deadlineStr, out DateTime deadline))
            {
                PrintError("Invalid date.");
                Pause();
                return;
            }

            var assignment = new Assignment(name, desc ?? "", deadline);
            var result = _courseService.AddCurriculum(code, assignment);

            if (result.Success)
                Console.WriteLine("\nAssignment added to curriculum.");
            else
                PrintError(result.Error);

            Pause();
        }

        private void ShowCurriculum()
        {
            PrintHeader("Course Curriculum");

            string? code = Prompt("Course code");

            if (string.IsNullOrWhiteSpace(code))
            {
                PrintError("Course code cannot be empty.");
                Pause();
                return;
            }

            var items = _courseService.GetCurriculum(code);

            if (!items.Any())
            {
                Console.WriteLine("No curriculum registered for this course.");
            }
            else
            {
                Console.WriteLine($"\n{"ID",-6} {"Name",-30} {"Deadline",-12} {"Description"}");
                Console.WriteLine(new string('-', 70));

                foreach (var a in items)
                {
                    Console.WriteLine($"{a.Id,-6} {a.Name,-30} {a.Deadline:yyyy-MM-dd,-12} {a.Description}");
                }
            }

            Pause();
        }

        private void SearchCourse()
        {
            PrintHeader("Search Courses");

            string? query = Prompt("Search (code or name)");

            if (string.IsNullOrWhiteSpace(query))
            {
                PrintError("Search term cannot be empty.");
                Pause();
                return;
            }

            var result = _courseService.SearchCourses(query);

            if (!result.Success)
            {
                PrintError(result.Error);
            }
            else
            {
                Console.WriteLine($"\n{"Code",-10} {"Name",-30} {"Credits",-8} {"Enrolled",-10}");
                Console.WriteLine(new string('-', 60));

                foreach (var c in result.Value)
                {
                    Console.WriteLine($"{c.Code,-10} {c.Name,-30} {c.Credits,-8} {c.EnrolledStudents.Count}/{c.MaxStudents}");
                }
            }

            Pause();
        }

        private static bool IsValidGrade(string grade) =>
            new[] { "A", "B", "C", "D", "F" }.Contains(grade);
    }
}