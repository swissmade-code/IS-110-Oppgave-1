using UniversitetSystem.Interfaces;

namespace UniversitetSystem.Domain.Library
{
    public class Loan
    {
        public LibraryItem Item { get; }
        public IBorrower Borrower { get; }
        public DateTime LoanDate { get; }
        public DateTime? ReturnDate { get; private set; }

        public bool IsActive => ReturnDate == null;

        public Loan(LibraryItem item, IBorrower borrower)
        {
            Item = item;
            Borrower = borrower;
            LoanDate = DateTime.Now;
        }

        public void Return()
        {
            ReturnDate = DateTime.Now;
        }
    }
}