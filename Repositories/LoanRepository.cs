using UniversitetSystem.Data;
using UniversitetSystem.Interfaces;
using UniversitetSystem.Domain.Library;

namespace UniversitetSystem.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        public void Add(Loan loan)
        {
            Database.Loans.Add(loan);
        }

        public Loan? GetActiveLoan(int itemId, int borrowerId)
        {
            return Database.Loans.FirstOrDefault(l =>
                l.Item.Id == itemId &&
                l.Borrower.ID == borrowerId &&
                l.IsActive);
        }

        public bool HasActiveLoan(int itemId, int borrowerId)
        {
            return Database.Loans.Any(l =>
                l.Item.Id == itemId &&
                l.Borrower.ID == borrowerId &&
                l.IsActive);
        }

        public List<Loan> GetActiveLoansByUser(int borrowerId)
        {
            return Database.Loans
                .Where(l => l.Borrower.ID == borrowerId && l.IsActive)
                .ToList();
        }

        public List<Loan> GetAllActiveLoans()
        {
            return Database.Loans.Where(l => l.IsActive).ToList();
        }

        public List<Loan> GetAllLoanHistory()
        {
            return Database.Loans
                .OrderByDescending(l => l.LoanDate)
                .ToList();
        }
    }
}