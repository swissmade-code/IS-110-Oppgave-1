using UniversitetSystem.Domain.Courses;
using UniversitetSystem.Domain.Library;
using UniversitetSystem.Interfaces;
using static UniversitetSystem.Helpers.ConsoleHelper;

namespace UniversitetSystem.Helpers
{
    public static class LibraryHelper
    {
        public static void SearchCourse(ICourseService courseService)
        {
            PrintHeader("Search Courses");

            string? query = Prompt("Search (course code or name)");

            if (string.IsNullOrWhiteSpace(query))
            {
                PrintError("Search query cannot be empty.");
                Pause();
                return;
            }

            var result = courseService.SearchCourses(query);

            if (!result.Success || result.Value.Count == 0)
            {
                PrintError(result.Error!);
                Pause();
                return;
            }

            PrintCourseList(result.Value);
            Pause();
        }

        public static void SearchBook(ILibraryService libraryService)
        {
            PrintHeader("Search Books");

            string? query = Prompt("Search (title or author)");

            if (string.IsNullOrWhiteSpace(query))
            {
                PrintError("Search query cannot be empty.");
                Pause();
                return;
            }

            var result = libraryService.Search(query);

            if (!result.Success || result.Value.Count == 0)
            {
                PrintError(result.Error!);
                Pause();
                return;
            }

            PrintBookList(result.Value);
            Pause();
        }

        public static void BorrowBook(ILoanService loanService, int userId)
        {
            PrintHeader("Borrow Book");

            int? itemId = PromptInt("Book ID");

            if (itemId == null)
            {
                PrintError("Invalid ID.");
                Pause();
                return;
            }

            var result = loanService.Borrow(userId, itemId.Value);

            if (!result.Success)
                PrintError(result.Error!);
            else
                Console.WriteLine("\nBook has been successfully borrowed.");

            Pause();
        }

        public static void ReturnBook(ILoanService loanService, int userId)
        {
            PrintHeader("Return Book");

            var loansResult = loanService.GetActiveLoansByUser(userId);

            if (!loansResult.Success)
            {
                PrintError(loansResult.Error!);
                Pause();
                return;
            }

            var activeLoans = loansResult.Value;

            if (!activeLoans.Any())
            {
                Console.WriteLine("You have no active loans.");
                Pause();
                return;
            }

            // Added Item ID column
            Console.WriteLine($" {"ID",-6} {"Title",-30} {"Loaned",-12}");
            Console.WriteLine(new string('-', 50));

            foreach (var loan in activeLoans)
            {
                Console.WriteLine($"{loan.Item.Id,-6} {loan.Item.Title,-30} {loan.LoanDate:yyyy-MM-dd}");
            }

            int? itemID = PromptInt("\nEnter Item ID to return");

            if (itemID == null)
            {
                PrintError("Invalid Item ID.");
                Pause();
                return;
            }

            var returnResult = loanService.Return(userId, itemID.Value);

            if (!returnResult.Success)
                PrintError(returnResult.Error!);
            else
                Console.WriteLine("\nBook returned successfully. Thank you!");

            Pause();
        }

        private static void PrintCourseList(IEnumerable<Course> courses)
        {
            var list = courses.ToList();
            if (!list.Any())
            {
                Console.WriteLine("No courses found.");
                return;
            }

            Console.WriteLine($"\n{"Code",-10} {"Name",-30} {"Credits",-8} {"Enrolled",-10}");
            Console.WriteLine(new string('-', 60));

            foreach (var c in list)
            {
                Console.WriteLine($"{c.Code,-10} {c.Name,-30} {c.Credits,-8} {c.EnrolledStudents.Count}/{c.MaxStudents}");
            }
        }

        private static void PrintBookList(IEnumerable<LibraryItem> items)
        {
            var list = items.ToList();
            if (!list.Any())
            {
                Console.WriteLine("No books found.");
                return;
            }

            Console.WriteLine($"\n{"ID",-6} {"Title",-30} {"Author",-20} {"Available",-8}");
            Console.WriteLine(new string('-', 66));

            foreach (var item in list)
            {
                Console.WriteLine($"{item.Id,-6} {item.Title,-30} {item.Author,-20} {item.AvailableCopies}/{item.TotalCopies}");
            }
        }
    }
}
