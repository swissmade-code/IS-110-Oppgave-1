

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
    }
}