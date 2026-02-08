using UniversitetSystem.Data;
using UniversitetSystem.Managers;
using UniversitetSystem.Models.Courses;


bool running = true;


// Seed sample data
DataSeeder.Seed();


while (running)
{
    Console.WriteLine();
    Console.WriteLine("[1] Create course");
    Console.WriteLine("[2] Enroll student in course");
    Console.WriteLine("[3] Print courses and participants");
    Console.WriteLine("[4] Search courses");
    Console.WriteLine("[0] Exit");

    Console.Write("Choose an option: ");
    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            CreateCourse();
            break;
        case "2":
            EnrollStudent();
            break;
        case "3":
            PrintCourses();
            break;
        case "4":
            SearchCourses();
            break;
        case "0":
            running = false;
            break;
        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}

static void CreateCourse()
{
    Console.Write("Course code: ");
    string code = Console.ReadLine() ?? "";

    Console.Write("Course name: ");
    string name = Console.ReadLine() ?? "";

    Console.Write("Credits: ");
    int credits = int.Parse(Console.ReadLine() ?? "0");

    Console.Write("Maximum number of students: ");
    int maxStudents = int.Parse(Console.ReadLine() ?? "0");

    var course = new Course(code, name, credits, maxStudents);

    if (!CourseManager.AddCourse(course))
        Console.WriteLine($"Course with code {code} already exists.");
    else
        Console.WriteLine($"Course {name} created successfully.");
}

static void EnrollStudent()
{
    var student = UserManager.SelectStudent();
    var course = CourseManager.SelectCourse();

    if (student == null || course == null) return;

    if (!course.EnrollStudent(student))
    {
        Console.WriteLine($"Could not enroll {student.Name} in {course.Name}.");
    }
    else
    {
        Console.WriteLine($"{student.Name} was successfully enrolled in {course.Name}.");
    }
}


static void PrintCourses()
{
    if (!CourseManager.Courses.Any())
    {
        Console.WriteLine("No courses registered.");
        return;
    }

    foreach (var course in CourseManager.Courses)
    {
        course.PrintCourseDetails();
        Console.WriteLine();
    }
}

static void SearchCourses()
{
    Console.Write("Search by code or name: ");
    string query = Console.ReadLine() ?? "";

    var results = CourseManager.Courses
        .Where(c => c.Code.Contains(query, StringComparison.OrdinalIgnoreCase)
                 || c.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
        .ToList();

    if (!results.Any())
        Console.WriteLine("No courses found.");
    else
        foreach (var course in results)
        {
            course.PrintCourseDetails();
            Console.WriteLine();
        }
}
