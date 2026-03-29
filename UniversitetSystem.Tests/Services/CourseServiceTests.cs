using UniversitetSystem.Services;
using UniversitetSystem.Domain.Students;
using UniversitetSystem.Tests.Repositories;
using UniversitetSystem.Domain.Users.Employees;
using UniversitetSystem.Enums;

public class CourseServiceTests
{
    [Fact]
    public void Enroll_WhenCourseIsFull_ReturnsFailure()
    {
        var userRepo = new InMemoryUserRepository();
        var courseRepo = new InMemoryCourseRepository();
        var courseService = new CourseService(courseRepo, userRepo);

        var teacher = new Teacher(1, "Prof X", "x@example.com", "pass", EmployeePosition.Lecturer, Department.ComputerScience);
        userRepo.AddUser(teacher);

        var student1 = new Student(2, "Student1", "s1@example.com", "pass");
        var student2 = new Student(3, "Student2", "s2@example.com", "pass");
        var student3 = new Student(4, "Student3", "s3@example.com", "pass");

        userRepo.AddUser(student1);
        userRepo.AddUser(student2);
        userRepo.AddUser(student3);

        var result = courseService.CreateCourse("CS101", "Intro CS", 5, 2, teacher.ID);
        var course = result.Value;

        Assert.NotNull(course);

        Assert.True(courseService.Enroll(student1.ID, "CS101").Success);
        Assert.True(courseService.Enroll(student2.ID, "CS101").Success);

        // Course is full now
        var enrollResult = courseService.Enroll(student3.ID, "CS101");

        Assert.False(enrollResult.Success);
        Assert.Contains("is full.", enrollResult.Error);

    }

    [Fact]
    public void Enroll_WhenStudentAlreadyEnrolled_ReturnsFailure()
    {
        var userRepo = new InMemoryUserRepository();
        var courseRepo = new InMemoryCourseRepository();
        var courseService = new CourseService(courseRepo, userRepo);

        var teacher = new Teacher(1, "Prof Y", "y@example.com", "pass", EmployeePosition.Lecturer, Department.IT);
        var student = new Student(2, "StudentA", "a@example.com", "pass");

        userRepo.AddUser(teacher);
        userRepo.AddUser(student);

        courseService.CreateCourse("IT101", "Intro IT", 5, 30, teacher.ID);

        var firstEnroll = courseService.Enroll(student.ID, "IT101");
        var secondEnroll = courseService.Enroll(student.ID, "IT101");

        Assert.True(firstEnroll.Success);
        Assert.False(secondEnroll.Success);
        Assert.Contains("already enrolled", secondEnroll.Error);
    }
}