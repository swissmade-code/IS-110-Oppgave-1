using UniversitetSystem.Domain.Courses;

namespace UniversitetSystem.Interfaces
{
    public interface ICourseRepository
    {
        Course AddCourse(Course course);
        bool Exists(string courseCode);
        Course? GetByCode(string courseCode);
        List<Course> GetAllCourses();
        List<Course> GetByCodeOrName(string query);
        List<Course> GetCoursesByTeacher(int teacherId);
    }
}