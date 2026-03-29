using UniversitetSystem.Common;
using UniversitetSystem.Domain.Library;

namespace UniversitetSystem.Interfaces
{
    public interface ILoanService
    {
        Result Borrow(int userId, int itemId);
        Result<List<Loan>> GetActiveLoansByUser(int borrowerId);
        Result<List<Loan>> GetAllActiveLoans();
        Result<List<Loan>> GetLoanHistory();
        Result Return(int userId, int itemId);
    }
}