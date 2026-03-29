using Microsoft.Extensions.DependencyInjection;
using UniveritetSystem.Services;
using UniversitetSystem.Data;
using UniversitetSystem.Domain.Repositories;
using UniversitetSystem.Domain.Students;
using UniversitetSystem.Domain.Users;
using UniversitetSystem.Domain.Users.Employees;
using UniversitetSystem.Enums;
using UniversitetSystem.Interfaces;
using UniversitetSystem.Repositories;
using UniversitetSystem.Services;
using UniversitetSystem.UI;

// ── Build DI container ───────────────────────────────────────────────
var services = new ServiceCollection();

// Repositories
services.AddSingleton<IUserRepository, UserRepository>();
services.AddSingleton<ICourseRepository, CourseRepository>();
services.AddSingleton<ILibraryRepository, LibraryRepository>();
services.AddSingleton<ILoanRepository, LoanRepository>();

// Services
services.AddSingleton<IAuthService, AuthService>();
services.AddSingleton<IUserService, UserService>();
services.AddSingleton<ICourseService, CourseService>();
services.AddSingleton<ILibraryService, LibraryService>();
services.AddSingleton<ILoanService, LoanService>();

// Menus
services.AddTransient<AuthMenu>();

var provider = services.BuildServiceProvider();

// ── Seed data ────────────────────────────────────────────────────────
DataSeeder.Seed(
    provider.GetRequiredService<IUserService>(),
    provider.GetRequiredService<ICourseService>(),
    provider.GetRequiredService<ILibraryService>()
);

// ── Main loop ────────────────────────────────────────────────────────
var authMenu = provider.GetRequiredService<AuthMenu>();

while (true)
{
    User? currentUser = authMenu.Run();

    if (currentUser == null)
    {
        Console.WriteLine("\nGoodbye!");
        break;
    }

    Console.Clear();
    Console.WriteLine($"Logged in as: {currentUser.Name} ({currentUser.Role})\n");

    switch (currentUser.Role)
    {
        case Role.Student:
            new StudentMenu(
                (Student)currentUser,
                provider.GetRequiredService<ICourseService>(),
                provider.GetRequiredService<ILibraryService>(),
                provider.GetRequiredService<ILoanService>()
            ).Run();
            break;

        case Role.Teacher:
            new TeacherMenu(
                (Teacher)currentUser,
                provider.GetRequiredService<ICourseService>(),
                provider.GetRequiredService<ILibraryService>(),
                provider.GetRequiredService<ILoanService>()
            ).Run();
            break;

        case Role.Librarian:
            new LibrarianMenu(
                (Librarian)currentUser,
                provider.GetRequiredService<ILibraryService>(),
                provider.GetRequiredService<ILoanService>()
            ).Run();
            break;

        default:
            Console.WriteLine($"[Warning] Unknown role: {currentUser.Role}");
            Console.ReadLine();
            break;
    }
}