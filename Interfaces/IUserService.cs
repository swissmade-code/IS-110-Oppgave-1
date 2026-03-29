using UniversitetSystem.Common;
using UniversitetSystem.Domain.Students;
using UniversitetSystem.Domain.Users;

namespace UniversitetSystem.Interfaces
{
    public interface IUserService
    {
        Result AddUser(User user);
        Result<List<IBorrower>> GetAllBorrowers();
        Result<List<Student>> GetAllStudents();
        Result<List<User>> GetAllUsers();
        int GetNextUserId();
    }
}