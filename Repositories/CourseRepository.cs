using UniversitetSystem.Data;
using UniversitetSystem.Interfaces;
using UniversitetSystem.Domain.Courses;

namespace UniversitetSystem.Domain.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        public Course AddCourse(Course course)
        {
            Database.Courses.Add(course);

            return course;
        }

        public bool Exists(string courseCode)
        {
            if (Database.Courses.Any(c => c.Code == courseCode))
            {
                return true;
            }

            return false;
        }

        public List<Course> GetByCodeOrName(string query)
        {
            var results = Database.Courses
                .Where(c => c.Code.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                            c.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return results;
        }

        public List<Course> GetAllCourses()
        {
            return Database.Courses.ToList();
        }

        public List<Course> GetCoursesByTeacher(int teacherId) => Database.Courses.Where(c => c.IsTaughtBy(teacherId)).ToList();

        public Course? GetByCode(string courseCode)
        {
            return Database.Courses.FirstOrDefault(c => c.Code.Contains(courseCode, StringComparison.OrdinalIgnoreCase));
        }
    }
}