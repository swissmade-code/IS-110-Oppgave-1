using UniversitetSystem.Common;
using UniversitetSystem.Domain.Students;

namespace UniversitetSystem.Domain.Courses
{
    public class Course
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public int Credits { get; private set; }
        public int MaxStudents { get; private set; }

        private List<int> _teacherIds = [];
        public IReadOnlyList<int> TeacherIds => _teacherIds.AsReadOnly();

        private List<Student> _enrolledStudents = [];
        public IReadOnlyList<Student> EnrolledStudents => _enrolledStudents.AsReadOnly();

        private List<Assignment> _curriculum = [];
        public IReadOnlyList<Assignment> Curriculum => _curriculum.AsReadOnly();

        public Course(string code, string name, int credits, int maxStudents)
        {
            Code = code;
            Name = name;
            Credits = credits;
            MaxStudents = maxStudents;
        }

        public Result EnrollStudent(Student student)
        {
            if (_enrolledStudents.Count >= MaxStudents)
            {
                return Result.Fail($"Cannot enroll {student.Name}: course is full.");
            }

            if (_enrolledStudents.Contains(student))
            {
                return Result.Fail($"{student.Name} is already enrolled in {Name}.");
            }

            _enrolledStudents.Add(student);

            return Result.Ok();
        }

        public Result RemoveStudent(Student student)
        {
            if (!_enrolledStudents.Contains(student))
            {
                return Result.Fail($"{student.Name} is not enrolled in {Name}.");
            }

            _enrolledStudents.Remove(student);

            return Result.Ok();
        }

        public Result AddTeacher(int teacherId)
        {
            if (_teacherIds.Contains(teacherId))
                return Result.Fail("Teacher is already assigned to this course.");
            _teacherIds.Add(teacherId);
            return Result.Ok();
        }

        public bool IsTaughtBy(int teacherId) => _teacherIds.Contains(teacherId);

        public Result AddAssignment(Assignment assignment)
        {
            if (_curriculum.Any(a => a.Name == assignment.Name))
                return Result.Fail($"Assignment '{assignment.Name}' already exists in this course.");

            _curriculum.Add(assignment);
            return Result.Ok();
        }

        public Result SetGrade(Guid assignmentId, int studentId, string grade)
        {
            var assignment = _curriculum.FirstOrDefault(a => a.Id == assignmentId);
            if (assignment is null)
                return Result.Fail("Assignment not found.");

            if (!_enrolledStudents.Any(s => s.ID == studentId))
                return Result.Fail("Student is not enrolled in this course.");

            return assignment.SetGrade(studentId, grade);
        }

        public Assignment? FindAssignment(Guid assignmentId) => _curriculum.FirstOrDefault(a => a.Id == assignmentId);
    }
}
