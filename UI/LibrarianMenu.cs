using UniversitetSystem.Domain.Library;
using UniversitetSystem.Domain.Users.Employees;
using UniversitetSystem.Enums;
using UniversitetSystem.Helpers;
using UniversitetSystem.Interfaces;
using static UniversitetSystem.Helpers.ConsoleHelper;

namespace UniversitetSystem.UI
{
    public class LibrarianMenu
    {
        private readonly Librarian _librarian;
        private readonly ILibraryService _libraryService;
        private readonly ILoanService _loanService;

        public LibrarianMenu(Librarian librarian, ILibraryService libraryService, ILoanService loanService)
        {
            _librarian = librarian;
            _libraryService = libraryService;
            _loanService = loanService;
        }

        public void Run()
        {
            while (true)
            {
                PrintHeader($"Librarian: {_librarian.Name}");

                Console.WriteLine("[1] Register Book/Media");
                Console.WriteLine("[2] Search Book");
                Console.WriteLine("[3] View All Active Loans");
                Console.WriteLine("[4] View Loan History");
                Console.WriteLine("[5] Borrow Book (for myself)");
                Console.WriteLine("[6] Return Book");
                Console.WriteLine("[0] Logout");

                string? input = Prompt("\nSelect");

                switch (input)
                {
                    case "1": RegisterItem(); break;
                    case "2": LibraryHelper.SearchBook(_libraryService); break;
                    case "3": ShowActiveLoans(); break;
                    case "4": ShowLoanHistory(); break;
                    case "5": LibraryHelper.BorrowBook(_loanService, _librarian.ID); break;
                    case "6": LibraryHelper.ReturnBook(_loanService, _librarian.ID); break;
                    case "0": return;
                    default:
                        PrintError("Invalid selection.");
                        Pause();
                        break;
                }
            }
        }

        private void RegisterItem()
        {
            PrintHeader("Register Book/Media");

            string? title = Prompt("Title");
            string? author = Prompt("Author");
            int? year = PromptInt("Year of Publication");
            int? copies = PromptInt("Number of Copies");
            int? typeValue = PromptInt("Media Type (1 = Book, 2 = DVD, 3 = Magazine)");

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
            {
                PrintError("Title and author are required.");
                Pause();
                return;
            }

            if (year == null || year < 1000 || year > DateTime.Now.Year)
            {
                PrintError("Invalid publication year.");
                Pause();
                return;
            }

            if (copies == null || copies <= 0)
            {
                PrintError("Number of copies must be a positive integer.");
                Pause();
                return;
            }

            if (typeValue == null || typeValue < 1 || typeValue > 3)
            {
                PrintError("Invalid media type.");
                Pause();
                return;
            }

            var type = (MediaType)typeValue;

            int newId = _libraryService.GetNextLibraryItemId();
            var item = new LibraryItem(newId, title, author, year.Value, copies.Value, type);

            var result = _libraryService.AddLibraryItem(item);

            if (result.Success)
                Console.WriteLine($"\nItem '{item.Title}' registered with ID {item.Id} ({copies} copies).");
            else
                PrintError(result.Error);

            Pause();
        }

        private void ShowActiveLoans()
        {
            PrintHeader("Active Loans");

            var result = _loanService.GetAllActiveLoans();

            if (!result.Success)
            {
                PrintError(result.Error);
                Pause();
                return;
            }

            Console.WriteLine($"{"UserID",-10} {"User",-22} {"BookID",-8} {"Title",-26} {"Loaned",-12}");
            Console.WriteLine(new string('-', 90));

            foreach (var loan in result.Value)
            {
                Console.WriteLine($"{loan.Borrower.ID,-10} {loan.Borrower.Name,-22} {loan.Item.Id,-8} {loan.Item.Title,-26} {loan.LoanDate:yyyy-MM-dd}");
            }

            Pause();
        }

        private void ShowLoanHistory()
        {
            PrintHeader("Loan History");

            var result = _loanService.GetLoanHistory();

            if (!result.Success)
            {
                PrintError(result.Error);
                Pause();
                return;
            }

            Console.WriteLine($"{"UserID",-10} {"User",-22} {"BookID",-8} {"Title",-26} {"Returned",-12}");
            Console.WriteLine(new string('-', 90));

            foreach (var loan in result.Value)
            {
                string returned = loan.ReturnDate.HasValue
                    ? loan.ReturnDate.Value.ToString("yyyy-MM-dd")
                    : "Active";

                Console.WriteLine($"{loan.Borrower.ID,-10} {loan.Borrower.Name,-22} {loan.Item.Id,-8} {loan.Item.Title,-26} {returned,-12}");
            }

            Pause();
        }
    }
}