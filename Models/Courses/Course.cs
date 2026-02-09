

namespace UniversitetSystem.Models.Courses
{
    public class Course
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public int MaxStudents { get; set; }

        private List<Student> _enrolledStudents = new List<Student>();

        public Course(string code, string name, int credits, int maxStudents)
        {
            Code = code;
            Name = name;
            Credits = credits;
            MaxStudents = maxStudents;
        }

        public bool EnrollStudent(Student student)
        {
            if (_enrolledStudents.Count >= MaxStudents)
            {
                Console.WriteLine($"Cannot enroll {student.Name}: course is full.");
                return false;
            }

            if (_enrolledStudents.Contains(student))
            {
                Console.WriteLine($"{student.Name} is already enrolled in {Name}.");
                return false;
            }

            _enrolledStudents.Add(student);
            student.EnrollInCourse(this);

            return true;
        }

        public bool RemoveStudent(Student student)
        {
            if (!_enrolledStudents.Contains(student))
            {
                Console.WriteLine($"{student.Name} is not enrolled in {Name}.");
                return false;
            }

            _enrolledStudents.Remove(student);
            student.DropCourse(this);

            return true;
        }

        public void PrintDetails()
        {
            Console.WriteLine($"Course: {Code} - {Name} ({Credits} credits)");
            Console.WriteLine($"Max students: {MaxStudents}, Enrolled: {_enrolledStudents.Count}");
            Console.WriteLine("Students:");
            foreach (var s in _enrolledStudents)
            {
                Console.WriteLine($"- {s.Name} (ID: {s.ID})");
            }
        }
    }
}