using UniversitetSystem.Common;
using UniversitetSystem.Enums;
using UniversitetSystem.Domain.Users;

namespace UniversitetSystem.Interfaces
{
    public interface IAuthService
    {
        Result<User> Login(string email, string password);
        Result Register(string name, string email, string password, Role role, Department? department);
    }
}