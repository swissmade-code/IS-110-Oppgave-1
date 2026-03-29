using UniversitetSystem.Domain.Library;
using UniversitetSystem.Interfaces;

namespace UniversitetSystem.Tests.Repositories
{
    public class InMemoryLoanRepository : ILoanRepository
    {
        private readonly List<Loan> _loans = new();

        public void Add(Loan loan) => _loans.Add(loan);

        public Loan? GetActiveLoan(int itemId, int borrowerId) =>
            _loans.FirstOrDefault(l => l.Item.Id == itemId && l.Borrower.ID == borrowerId && l.IsActive);

        public bool HasActiveLoan(int itemId, int borrowerId) =>
            _loans.Any(l => l.Item.Id == itemId && l.Borrower.ID == borrowerId && l.IsActive);

        public List<Loan> GetActiveLoansByUser(int borrowerId) =>
            _loans.Where(l => l.Borrower.ID == borrowerId && l.IsActive).ToList();

        public List<Loan> GetAllActiveLoans() =>
            _loans.Where(l => l.IsActive).ToList();

        public List<Loan> GetAllLoanHistory() => _loans.ToList();
    }
}