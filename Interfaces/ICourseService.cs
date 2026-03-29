using UniversitetSystem.Common;
using UniversitetSystem.Domain.Courses;
using UniversitetSystem.Domain.Students;

namespace UniversitetSystem.Interfaces
{
    public interface ICourseService
    {
        Result AddCurriculum(string courseCode, Assignment assignment);
        Result<Course> CreateCourse(string code, string name, int credits, int maxStudents, int teacherId);
        Result Enroll(int studentId, string code);
        List<Course> GetCoursesByTeacher(int teacherId);
        List<Assignment> GetCurriculum(string courseCode);
        List<(Student Student, string? Grade)> GetEnrollmentsForCourse(string code);
        Result<List<Course>> GetStudentCourses(int studentId);
        Result<List<(Course course, string grade)>> GetStudentGrades(int studentId);
        Result<List<Course>> SearchCourses(string query);
        Result SetGrade(int studentId, string courseCode, Guid assignmentId, string grade);
        Result UnEnroll(int studentId, string code);
    }
}