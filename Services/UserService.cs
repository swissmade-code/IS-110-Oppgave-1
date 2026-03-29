using UniversitetSystem.Common;
using UniversitetSystem.Interfaces;
using UniversitetSystem.Domain.Students;
using UniversitetSystem.Domain.Users;

namespace UniversitetSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Result AddUser(User user)
        {
            if (_userRepository.ExistsById(user.ID))
                return Result.Fail("User with this ID already exists.");

            _userRepository.AddUser(user);
            return Result.Ok();
        }

        public Result<List<User>> GetAllUsers()
        {
            var users = _userRepository.GetAll();

            if (!users.Any())
                return Result<List<User>>.Fail("No users registered.");

            return Result<List<User>>.Ok(users);
        }

        public Result<List<IBorrower>> GetAllBorrowers()
        {
            var borrowers = _userRepository.GetAll()
                .OfType<IBorrower>()
                .ToList();

            if (!borrowers.Any())
                return Result<List<IBorrower>>.Fail("No borrowers registered.");

            return Result<List<IBorrower>>.Ok(borrowers);
        }

        public Result<List<Student>> GetAllStudents()
        {
            var students = _userRepository.GetAll()
                .OfType<Student>()
                .ToList();

            if (!students.Any())
                return Result<List<Student>>.Fail("No students registered.");

            return Result<List<Student>>.Ok(students);
        }

        public int GetNextUserId()
        {
            return _userRepository.GetNextUserId();
        }
    }
}