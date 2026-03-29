
using UniversitetSystem.Domain.Library;
using UniversitetSystem.Domain.Students;
using UniversitetSystem.Enums;
using UniversitetSystem.Services;
using UniversitetSystem.Tests.Repositories;

namespace UniversitetSystem.Tests.Services
{
    public class LibraryServiceTests
    {
        [Fact]
        public void Borrow_WhenNoCopiesAvailable_ReturnsFailure()
        {
            var userRepo = new InMemoryUserRepository();
            var libraryRepo = new InMemoryLibraryRepository();
            var loanRepo = new InMemoryLoanRepository();
            var loanService = new LoanService(userRepo, libraryRepo, loanRepo);

            var student = new Student(1, "Borrower", "b@example.com", "pass");
            userRepo.AddUser(student);

            var book = new LibraryItem(1, "Empty Book", "Author", 2020, 0, MediaType.Book);
            libraryRepo.Add(book);

            var result = loanService.Borrow(student.ID, book.Id);

            Assert.False(result.Success);
            Assert.Contains("No copies", result.Error);
        }
    }
}