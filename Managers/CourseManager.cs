

namespace UniversitetSystem.Models.Courses
{
    public static class CourseManager
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

        public static List<Course> Search(string query)
        {
            return _courses.Where(c => c.Code.Contains(query, StringComparison.OrdinalIgnoreCase) || c.Name.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
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

        public static void PrintAllCourses()
        {
            if (!_courses.Any())
            {
                Console.WriteLine("No courses registered.");
                return;
            }

            foreach (var course in _courses)
            {
                course.PrintDetails();
                Console.WriteLine("*-------------------------*");
            }
        }
    }
}
