using UniversitetSystem.Common;
using UniversitetSystem.Domain.Courses;
using UniversitetSystem.Domain.Students;
using UniversitetSystem.Interfaces;
using UniversitetSystem.Enums;

namespace UniversitetSystem.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;

        public CourseService(ICourseRepository courseRepository, IUserRepository userRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }

        public Result Enroll(int studentId, string code)
        {
            var course = _courseRepository.GetByCode(code);
            if (course == null) return Result.Fail("Course not found.");

            var user = _userRepository.GetById(studentId);
            if (user == null) return Result.Fail("User not found.");
            if (user is not Student student) return Result.Fail("Not a student.");

            return course.EnrollStudent(student);
        }

        public Result UnEnroll(int studentId, string code)
        {
            var course = _courseRepository.GetByCode(code);
            if (course == null) return Result.Fail("Course not found.");

            var user = _userRepository.GetById(studentId);
            if (user == null) return Result.Fail("User not found.");
            if (user is not Student student) return Result.Fail("Not a student.");

            return course.RemoveStudent(student);
        }

        public Result<List<Course>> GetStudentCourses(int studentId)
        {
            var courses = _courseRepository.GetAllCourses()
                .Where(c => c.EnrolledStudents.Any(s => s.ID == studentId))
                .ToList();

            return Result<List<Course>>.Ok(courses);
        }

        public Result<List<(Course course, string grade)>> GetStudentGrades(int studentId)
        {
            var coursesResult = GetStudentCourses(studentId);
            if (!coursesResult.Success || !coursesResult.Value.Any())
                return Result<List<(Course, string)>>.Fail("You are not enrolled in any courses.");

            var gradedCourses = new List<(Course, string)>();
            foreach (var course in coursesResult.Value)
            {
                foreach (var assignment in course.Curriculum)
                {
                    var grade = assignment.GetGrade(studentId);
                    if (grade != null)
                    {
                        gradedCourses.Add((course, grade));
                        break;
                    }
                }
            }

            if (!gradedCourses.Any())
                return Result<List<(Course, string)>>.Fail("You have no grades yet.");

            return Result<List<(Course, string)>>.Ok(gradedCourses);
        }

        public Result<List<Course>> SearchCourses(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return Result<List<Course>>.Fail("Search query cannot be empty.");

            var courses = _courseRepository.GetByCodeOrName(query);
            if (!courses.Any())
                return Result<List<Course>>.Fail("No courses found matching your query.");

            return Result<List<Course>>.Ok(courses);
        }

        public Result<Course> CreateCourse(string code, string name, int credits, int maxStudents, int teacherId)
        {
            if (_courseRepository.Exists(code))
                return Result<Course>.Fail("Course with this code already exists.");

            var teacher = _userRepository.GetById(teacherId);
            if (teacher == null)
                return Result<Course>.Fail("Teacher not found.");

            if (teacher.Role != Role.Teacher)
                return Result<Course>.Fail("User is not a teacher.");

            var course = new Course(code, name, credits, maxStudents);
            course.AddTeacher(teacherId);

            _courseRepository.AddCourse(course);

            return Result<Course>.Ok(course);
        }

        public List<Course> GetCoursesByTeacher(int teacherId)
        {
            return _courseRepository.GetCoursesByTeacher(teacherId);
        }

        public List<(Student Student, string? Grade)> GetEnrollmentsForCourse(string code)
        {
            var course = _courseRepository.GetByCode(code);
            if (course == null) return new List<(Student, string?)>();

            return course.EnrolledStudents
                .Select(s =>
                {
                    string? grade = course.Curriculum
                        .Select(a => a.GetGrade(s.ID))
                        .FirstOrDefault(g => g != null);

                    return (s, grade);
                })
                .ToList();
        }

        public Result SetGrade(int studentId, string courseCode, Guid assignmentId, string grade)
        {
            var course = _courseRepository.GetByCode(courseCode);
            if (course == null) return Result.Fail("Course not found.");

            var student = _userRepository.GetById(studentId) as Student;
            if (student == null) return Result.Fail("Student not found.");

            return course.SetGrade(assignmentId, studentId, grade);
        }

        public Result AddCurriculum(string courseCode, Assignment assignment)
        {
            var course = _courseRepository.GetByCode(courseCode);
            if (course == null) return Result.Fail("Course not found.");

            return course.AddAssignment(assignment);
        }

        public List<Assignment> GetCurriculum(string courseCode)
        {
            var course = _courseRepository.GetByCode(courseCode);
            return course?.Curriculum.ToList() ?? new List<Assignment>();
        }
    }
}