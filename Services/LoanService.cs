using UniversitetSystem.Common;
using UniversitetSystem.Interfaces;
using UniversitetSystem.Domain.Library;
using UniversitetSystem.Domain.Exceptions;

namespace UniversitetSystem.Services
{
    public class LoanService : ILoanService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILibraryRepository _libraryRepository;
        private readonly ILoanRepository _loanRepository;

        public LoanService(
            IUserRepository userRepository,
            ILibraryRepository libraryRepository,
            ILoanRepository loanRepository)
        {
            _userRepository = userRepository;
            _libraryRepository = libraryRepository;
            _loanRepository = loanRepository;
        }

        public Result Borrow(int userId, int itemId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                return Result.Fail("User not found.");

            if (user is not IBorrower borrower)
                return Result.Fail("User cannot borrow.");

            var item = _libraryRepository.GetById(itemId);
            if (item == null)
                return Result.Fail("Item not found.");

            if (_loanRepository.HasActiveLoan(itemId, borrower.ID))
                return Result.Fail("User already borrowed this item.");

            try
            {
                var loan = new Loan(item, borrower);
                _loanRepository.Add(loan);
                item.Borrow();
            }
            catch (LibraryItemUnavailableException ex)
            {
                return Result.Fail(ex.Message);
            }

            return Result.Ok();
        }

        public Result Return(int userId, int itemId)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                return Result.Fail("User not found.");

            if (user is not IBorrower borrower)
                return Result.Fail("User cannot return.");

            var item = _libraryRepository.GetById(itemId);
            if (item == null)
                return Result.Fail("Item not found.");

            var loan = _loanRepository.GetActiveLoan(itemId, borrower.ID);
            if (loan == null)
                return Result.Fail("No active loan found.");

            try
            {
                item.Return();
                loan.Return();
            }
            catch (LibraryItemAlreadyReturnedException ex)
            {
                return Result.Fail(ex.Message);
            }


            return Result.Ok();
        }

        public Result<List<Loan>> GetActiveLoansByUser(int borrowerId)
        {
            var loans = _loanRepository.GetActiveLoansByUser(borrowerId);

            if (loans == null || !loans.Any())
                return Result<List<Loan>>.Fail("You have no active loans.");

            return Result<List<Loan>>.Ok(loans);
        }

        public Result<List<Loan>> GetAllActiveLoans()
        {
            var loans = _loanRepository.GetAllActiveLoans();

            if (loans == null || !loans.Any())
                return Result<List<Loan>>.Fail("There is no active loans.");

            return Result<List<Loan>>.Ok(loans);
        }

        public Result<List<Loan>> GetLoanHistory()
        {
            var loans = _loanRepository.GetAllLoanHistory();

            if (loans == null || !loans.Any())
                return Result<List<Loan>>.Fail("There is no active loans.");

            return Result<List<Loan>>.Ok(loans);
        }
    }
}