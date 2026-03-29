using UniversitetSystem.Domain.Courses;
using UniversitetSystem.Domain.Students;
using UniversitetSystem.Domain.Users.Employees;
using UniversitetSystem.Enums;
using UniversitetSystem.Services;
using UniversitetSystem.Tests.Repositories;

public class UserServiceTests
{
    [Fact]
    public void Teacher_CanAssignGrade_StudentCanViewIt()
    {
        // Repos
        var userRepo = new InMemoryUserRepository();
        var courseRepo = new InMemoryCourseRepository();
        var courseService = new CourseService(courseRepo, userRepo);

        // Users
        var teacher = new Teacher(1, "Prof Z", "z@example.com", "pass", EmployeePosition.Lecturer, Department.ComputerScience);
        var student = new Student(2, "StudentB", "b@example.com", "pass");

        userRepo.AddUser(teacher);
        userRepo.AddUser(student);

        // Create Course
        var course = courseService.CreateCourse("CS102", "Data Structures", 5, 30, teacher.ID).Value!;

        // Add an assignment
        var assignment = new Assignment("Homework 1", "First assignment", DateTime.Now.AddDays(7));
        course.AddAssignment(assignment);

        // Enroll student
        courseService.Enroll(student.ID, "CS102");

        // Set grade
        var gradeResult = courseService.SetGrade(student.ID, "CS102", assignment.Id, "A");
        Assert.True(gradeResult.Success);

        // Verify grade
        var studentGrade = assignment.GetGrade(student.ID);
        Assert.Equal("A", studentGrade);
    }
}