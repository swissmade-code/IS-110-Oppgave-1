using UniversitetSystem.Common;

namespace UniversitetSystem.Domain.Courses
{
    public class Assignment
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public DateTime Deadline { get; private set; }
        public string Description { get; private set; }


        private static readonly HashSet<string> ValidGrades = ["A", "B", "C", "D", "F"];
        private Dictionary<int, string> _grades = new();

        public Assignment(string name, string description, DateTime deadline)
        {
            Name = name;
            Description = description;
            Deadline = deadline;
        }

        public Result SetGrade(int studentId, string grade)
        {
            if (!ValidGrades.Contains(grade))
            {
                return Result.Fail($"Invalid grade: {grade}");
            }

            _grades[studentId] = grade;
            return Result.Ok();
        }

        public string? GetGrade(int studentId)
        {
            return _grades.TryGetValue(studentId, out var grade) ? grade : null;
        }
    }
}