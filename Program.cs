using UniversitetSystem.Data;
using UniversitetSystem.Enums;
using UniversitetSystem.Managers;
using UniversitetSystem.Models.Courses;
using UniversitetSystem.Models.Library;


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
    Console.WriteLine("[5] Search library item");
    Console.WriteLine("[6] Borrow library item");
    Console.WriteLine("[7] Return library item");
    Console.WriteLine("[8] Register library item");
    Console.WriteLine("[9] Print library items");
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
        case "5":
            SearchLibraryItems();
            break;
        case "6":
            BorrowLibraryItem();
            break;
        case "7":
            ReturnLibraryItem();
            break;
        case "8":
            RegisterLibraryItem();
            break;
        case "9":
            PrintAllLibraryItems();
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
    CourseManager.PrintAllCourses();
}

static void PrintAllLibraryItems()
{
    LibraryManager.PrintAllLibraryItems();
}

static void SearchCourses()
{
    Console.Write("Search by code or name: ");
    string query = Console.ReadLine() ?? "";

    var results = CourseManager.Search(query);

    if (!results.Any())
        Console.WriteLine("No courses found.");
    else
        foreach (var course in results)
        {
            course.PrintDetails();
            Console.WriteLine();
        }
}

static void SearchLibraryItems()
{
    Console.Write("Search by title or author: ");
    string query = Console.ReadLine() ?? "";

    var results = LibraryManager.Search(query);

    if (!results.Any())
        Console.WriteLine("No library items found.");
    else
        foreach (var libraryItem in results)
        {
            libraryItem.PrintDetails();
            Console.WriteLine();
        }
}

static void RegisterLibraryItem()
{
    Console.Write("Id: ");
    int id = int.Parse(Console.ReadLine()!);

    Console.Write("Title: ");
    string title = Console.ReadLine()!;

    Console.Write("Author: ");
    string author = Console.ReadLine()!;

    Console.Write("Year: ");
    int year = int.Parse(Console.ReadLine()!);

    Console.Write("Number of copies: ");
    int copies = int.Parse(Console.ReadLine()!);

    Console.Write("Media type (Book, DVD, Magazine): ");
    if (!Enum.TryParse<MediaType>(Console.ReadLine()!, true, out var type))
    {
        Console.WriteLine("Invalid media type.");
        return;
    }

    var item = new LibraryItem(id, title, author, year, copies, type);

    if (LibraryManager.AddLibraryItem(item))
    {
        Console.WriteLine("Library item registered.");
    }
    else
    {
        Console.WriteLine("Could not register library item.");
    }
}

static void BorrowLibraryItem()
{
    var borrower = UserManager.SelectBorrower();
    if (borrower == null) return;

    var item = LibraryManager.SelectLibraryItem();
    if (item == null) return;

    if (LibraryManager.BorrowItem(item.Id, borrower))
    {
        Console.WriteLine($"{borrower.Name} successfully borrowed \"{item.Title}\".");
    }
    else
    {
        Console.WriteLine($"Could not borrow \"{item.Title}\".");
    }

}

static void ReturnLibraryItem()
{
    var loan = LibraryManager.SelectActiveLoan();
    if (loan == null) return;

    if (LibraryManager.ReturnItem(loan.Item.Id, loan.Borrower))
    {
        Console.WriteLine($"{loan.Borrower.Name} returned \"{loan.Item.Title}\".");
    }
    else
    {
        Console.WriteLine($"Could not return \"{loan.Item.Title}\".");
    }
}
