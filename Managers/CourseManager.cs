

namespace UniversitetSystem.Models.Courses
{
    public class CourseManager
    {
        private static List<Course> _courses = new List<Course>();

        public static IReadOnlyCollection<Course> Courses = _courses;

        public static bool AddCourse(Course course)
        {
            if (_courses.Any(c => c.Code == course.Code))
            {
                return false;
            }

            _courses.Add(course);

            return true;
        }

        public static Course? FindByCode(string code)
        {
            return _courses.FirstOrDefault(c => c.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
        }

        public static List<Course> FindByName(string name)
        {
            return _courses.Where(c => c.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        public static Course? SelectCourse()
        {
            if (!_courses.Any())
            {
                Console.WriteLine("No _courses registered.");
                return null;
            }

            Console.WriteLine("Select a course:");
            for (int i = 0; i < _courses.Count; i++)
            {
                var c = _courses[i];
                Console.WriteLine($"[{i + 1}] {c.Code} - {c.Name}");
            }

            Console.Write("Your choice: ");
            if (int.TryParse(Console.ReadLine(), out int choice) &&
                choice > 0 && choice <= _courses.Count)
            {
                return _courses[choice - 1];
            }

            Console.WriteLine("Invalid choice.");
            return null;
        }

        public static void PrintAll_courses()
        {
            foreach (var course in _courses)
            {
                course.PrintCourseDetails();
                Console.WriteLine("*-------------------------*");
            }
        }
    }
}