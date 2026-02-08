using UniversitetSystem.Interfaces;
using UniversitetSystem.Models.Courses;

namespace UniversitetSystem.Models
{
    public class Student : User, IBorrower
    {
        private List<Course> _enrolledCourses = new();

        public IReadOnlyList<Course> EnrolledCourses => _enrolledCourses;

        public Student(int studentID, string name, string email) : base(studentID, name, email) { }

        public void EnrollInCourse(Course course)
        {
            if (!_enrolledCourses.Contains(course))
                _enrolledCourses.Add(course);
        }

        public void DropCourse(Course course)
        {
            _enrolledCourses.Remove(course);
        }
    }
}