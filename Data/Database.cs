using UniversitetSystem.Domain.Users;
using UniversitetSystem.Domain.Courses;
using UniversitetSystem.Domain.Library;

namespace UniversitetSystem.Data
{
    public static class Database
    {
        public static List<User> Users { get; } = new();
        public static List<Course> Courses { get; } = new();
        public static List<LibraryItem> LibraryItems { get; } = new();
        public static List<Loan> Loans { get; } = new();

        public static int NextUserId()
        {
            return Users.Count == 0 ? 1 : Users.Max(u => u.ID) + 1;
        }

        public static int NextLibraryItemId()
        {
            return LibraryItems.Count == 0 ? 1 : LibraryItems.Max(u => u.Id) + 1;
        }
    }
}