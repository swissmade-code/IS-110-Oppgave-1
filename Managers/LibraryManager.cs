

using UniversitetSystem.Interfaces;

namespace UniversitetSystem.Models.Library
{
    public static class LibraryManager
    {
        private static List<Loan> _loans = new();
        private static List<LibraryItem> _items = new();

        public static bool AddLibraryItem(LibraryItem item)
        {
            if (_items.Any(i => i.Id == item.Id))
            {
                return false;
            }

            _items.Add(item);

            return true;
        }

        public static bool BorrowItem(int itemId, IBorrower borrower)
        {
            var item = _items.FirstOrDefault(i => i.Id == itemId);

            if (item == null || !item.IsAvailable)
            {
                return false;
            }

            item.Borrow();
            _loans.Add(new Loan(item, borrower));

            return true;
        }

        public static bool ReturnItem(int itemId, IBorrower borrower)
        {
            var loan = _loans.FirstOrDefault(l => l.Borrower.ID == borrower.ID && l.Item.Id == itemId && l.IsActive);

            if (loan == null)
            {
                return false;
            }

            loan.Return();
            loan.Item.Return();

            return true;
        }

        public static List<LibraryItem> Search(string query)
        {
            return _items.Where(l => l.Title.Contains(query, StringComparison.OrdinalIgnoreCase) || l.Author.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public static LibraryItem? SelectLibraryItem()
        {
            if (!_items.Any())
            {
                Console.WriteLine("No library items registered.");
                return null;
            }

            Console.WriteLine("Select an library item:");
            for (int i = 0; i < _items.Count; i++)
            {
                var l = _items[i];
                Console.WriteLine($"[{i + 1}] {l.Title} (Author: {l.Author})");
            }

            Console.Write("Your choice: ");
            if (int.TryParse(Console.ReadLine(), out int choice) &&
                choice > 0 && choice <= _items.Count)
            {
                return _items[choice - 1];
            }

            Console.WriteLine("Invalid choice.");
            return null;
        }

        public static Loan? SelectActiveLoan()
        {
            var activeLoans = _loans.Where(l => l.IsActive).ToList();

            if (!activeLoans.Any())
            {
                Console.WriteLine("No active loans registered.");
                return null;
            }

            Console.WriteLine("Select a loan to return:");
            Console.WriteLine("*-------------------------*");

            for (int i = 0; i < activeLoans.Count; i++)
            {
                var loan = activeLoans[i];
                Console.WriteLine($"[{i + 1}] Borrower: {loan.Borrower.Name} | Item: {loan.Item.Title} ({loan.Item.Type}) | Loaned on: {loan.LoanDate:yyyy-MM-dd}");
            }

            Console.Write("Your choice: ");
            if (int.TryParse(Console.ReadLine(), out int choice) &&
                choice > 0 && choice <= activeLoans.Count)
            {
                return activeLoans[choice - 1];
            }

            Console.WriteLine("Invalid choice.");
            return null;
        }

        public static void PrintAllLibraryItems()
        {
            if (!_items.Any())
            {
                Console.WriteLine("No library items registered.");
                return;
            }

            Console.WriteLine("Library Items:");
            Console.WriteLine("*-------------------------*");

            foreach (var item in _items)
            {
                item.PrintDetails();
                Console.WriteLine("*-------------------------*");
            }
        }

        public static void PrintLoanHistory()
        {
            if (!_loans.Any())
            {
                Console.WriteLine("No loan history.");
                return;
            }

            Console.WriteLine("Loan History:");
            Console.WriteLine("*-------------------------*");

            foreach (var loan in _loans)
            {
                Console.WriteLine($"Borrower: {loan.Borrower.Name} (ID: {loan.Borrower.ID})");
                Console.WriteLine($"Item: {loan.Item.Title} by {loan.Item.Author}");
                Console.WriteLine($"Loan Date: {loan.LoanDate:yyyy-MM-dd}");
                Console.WriteLine($"Return Date: {(loan.ReturnDate.HasValue ? loan.ReturnDate.Value.ToString("yyyy-MM-dd") : "Not returned")}");
                Console.WriteLine($"Status: {(loan.IsActive ? "Active" : "Returned")}");
                Console.WriteLine("*-------------------------*");
            }
        }

        public static void PrintActiveLoans()
        {
            var activeLoans = _loans.Where(l => l.IsActive).ToList();

            if (!activeLoans.Any())
            {
                Console.WriteLine("No active loans.");
                return;
            }

            Console.WriteLine("Active Loans:");
            Console.WriteLine("*-------------------------*");

            foreach (var loan in activeLoans)
            {
                Console.WriteLine($"Borrower: {loan.Borrower.Name} (ID: {loan.Borrower.ID})");
                Console.WriteLine($"Item: {loan.Item.Title} by {loan.Item.Author}");
                Console.WriteLine($"Loan Date: {loan.LoanDate:yyyy-MM-dd}");
                Console.WriteLine("*-------------------------*");
            }
        }
    }
}