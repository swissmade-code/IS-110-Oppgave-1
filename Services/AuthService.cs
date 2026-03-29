using UniversitetSystem.Common;
using UniversitetSystem.Enums;
using UniversitetSystem.Factories;
using UniversitetSystem.Interfaces;
using UniversitetSystem.Domain.Users;

namespace UniveritetSystem.Services
{

    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Result<User> Login(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);

            if (user == null || user.Password != password)
            {
                return Result<User>.Fail("Wrong email or password.");
            }

            return Result<User>.Ok(user);
        }

        public Result Register(string name, string email, string password, Role role, Department? department)
        {
            if (_userRepository.ExistsByEmail(email))
            {
                return Result.Fail("User already exists.");
            }

            var user = UserFactory.CreateUser(role, _userRepository.GetNextUserId(), name, email, password, department);

            _userRepository.AddUser(user);

            return Result.Ok();
        }
    }
}
