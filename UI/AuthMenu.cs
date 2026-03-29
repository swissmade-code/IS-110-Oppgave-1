using UniversitetSystem.Domain.Users;
using UniversitetSystem.Enums;
using UniversitetSystem.Interfaces;
using UniversitetSystem.Helpers;

namespace UniversitetSystem.UI
{
    public class AuthMenu
    {
        private readonly IAuthService _authService;

        public AuthMenu(IAuthService authService)
        {
            _authService = authService;
        }

        public User? Run()
        {
            while (true)
            {
                ConsoleHelper.PrintHeader("Welcome to University System");

                Console.WriteLine("[1] Login");
                Console.WriteLine("[2] Register");
                Console.WriteLine("[0] Exit");

                string? input = ConsoleHelper.Prompt("\nSelect");

                switch (input)
                {
                    case "1":
                        var user = Login();
                        if (user != null) return user;
                        break;

                    case "2":
                        Register();
                        break;

                    case "0":
                        return null;

                    default:
                        ConsoleHelper.PrintError("Invalid choice.");
                        ConsoleHelper.Pause();
                        break;
                }
            }
        }

        private User? Login()
        {
            ConsoleHelper.PrintHeader("Login");

            string? email = ConsoleHelper.Prompt("Email");

            Console.Write("Password: ");
            string? password = ReadPassword();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ConsoleHelper.PrintError("Email and password cannot be empty.");
                ConsoleHelper.Pause();
                return null;
            }

            var result = _authService.Login(email, password);

            if (!result.Success)
            {
                ConsoleHelper.PrintError(result.Error);
                ConsoleHelper.Pause();
                return null;
            }

            Console.WriteLine($"\nWelcome, {result.Value.Name}! (Role: {result.Value.Role})");
            ConsoleHelper.Pause();

            return result.Value;
        }

        private void Register()
        {
            ConsoleHelper.PrintHeader("Register");

            Role role = PromptRole();

            string? name = PromptRequired("Name");
            string? email = PromptRequired("Email");
            string? password = PromptPassword();

            if (name == null || email == null || password == null) return;

            Department? department = null;

            if (role == Role.Teacher)
            {
                Console.WriteLine("\nSelect Department:");
                Console.WriteLine("[1] Mathematics");
                Console.WriteLine("[2] IT");

                string? depInput = ConsoleHelper.Prompt("Select");

                department = depInput switch
                {
                    "1" => Department.Mathematics,
                    "2" => Department.IT,
                    _ => Department.Mathematics
                };
            }

            var result = _authService.Register(name, email, password, role, department);

            if (!result.Success)
                ConsoleHelper.PrintError(result.Error);
            else
                Console.WriteLine("\nUser registered successfully!");

            ConsoleHelper.Pause();
        }

        // ---------- Helpers ----------

        private string? PromptRequired(string label)
        {
            string? value = ConsoleHelper.Prompt(label);

            if (string.IsNullOrWhiteSpace(value))
            {
                ConsoleHelper.PrintError($"{label} cannot be empty.");
                ConsoleHelper.Pause();
                return null;
            }

            return value;
        }
        private Role PromptRole()
        {
            while (true)
            {
                Console.WriteLine("Select role:");
                Console.WriteLine("[1] Student");
                Console.WriteLine("[2] Teacher");
                Console.WriteLine("[3] Librarian");

                string? input = ConsoleHelper.Prompt("\nSelect");

                switch (input)
                {
                    case "1": return Role.Student;
                    case "2": return Role.Teacher;
                    case "3": return Role.Librarian;
                    default:
                        ConsoleHelper.PrintError("Invalid selection. Please try again.");
                        ConsoleHelper.Pause();
                        break; // loop again
                }
            }
        }
        private string? PromptPassword()
        {
            Console.Write("Password: ");
            string? pw1 = ReadPassword();

            Console.Write("Confirm Password: ");
            string? pw2 = ReadPassword();

            if (pw1 != pw2)
            {
                ConsoleHelper.PrintError("Passwords do not match.");
                ConsoleHelper.Pause();
                return null;
            }

            if (string.IsNullOrWhiteSpace(pw1))
            {
                ConsoleHelper.PrintError("Password cannot be empty.");
                ConsoleHelper.Pause();
                return null;
            }

            return pw1;
        }

        private static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password[..^1];
                    Console.Write("\b \b");
                }

            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }
    }

}