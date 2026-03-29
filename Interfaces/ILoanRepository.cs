using UniversitetSystem.Domain.Library;

namespace UniversitetSystem.Interfaces
{
    public interface ILoanRepository
    {
        void Add(Loan loan);
        Loan? GetActiveLoan(int itemId, int borrowerId);
        bool HasActiveLoan(int itemId, int borrowerId);
        List<Loan> GetActiveLoansByUser(int borrowerId);
        List<Loan> GetAllActiveLoans();
        List<Loan> GetAllLoanHistory();
    }
}