using UniversitetSystem.Domain.Courses;
using UniversitetSystem.Interfaces;

namespace UniversitetSystem.Tests.Repositories
{
    public class InMemoryCourseRepository : ICourseRepository
    {
        private readonly List<Course> _courses = new();

        public Course AddCourse(Course course)
        {
            _courses.Add(course);
            return course;
        }

        public bool Exists(string courseCode) => _courses.Any(c => c.Code == courseCode);

        public List<Course> GetAllCourses() => _courses;

        public Course? GetByCode(string courseCode) => _courses.FirstOrDefault(c => c.Code == courseCode);

        public List<Course> GetByCodeOrName(string query) =>
            _courses.Where(c => c.Code.Contains(query) || c.Name.Contains(query)).ToList();

        public List<Course> GetCoursesByTeacher(int teacherId) =>
            _courses.Where(c => c.TeacherIds.Contains(teacherId)).ToList();
    }
}