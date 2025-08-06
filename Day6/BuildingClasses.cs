// This example demonstrates all 4 OOP principles via a university model
using System;
using System.Collections.Generic;

namespace Day6
{
    // 1. Interfaces
    public interface IPersonService
    {
        int CalculateAge();
        decimal CalculateSalary();
        List<string> GetAddresses();
    }

    public interface IStudentService : IPersonService
    {
        double CalculateGPA();
    }

    public interface IInstructorService : IPersonService
    {
        int GetYearsOfExperience();
    }

    public interface ICourseService
    {
        void EnrollStudent(Student student);
    }

    public interface IDepartmentService
    {
        Instructor Head { get; set; }
        decimal Budget { get; set; }
    }

    // 2. Abstract class for Abstraction + Inheritance
    public abstract class Person : IPersonService
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        private decimal _salary;
        private List<string> addresses = new List<string>();

        public Person(string name, DateTime birthDate)
        {
            Name = name;
            BirthDate = birthDate;
        }

        public int CalculateAge()
        {
            return DateTime.Now.Year - BirthDate.Year;
        }

        public virtual decimal CalculateSalary()
        {
            return _salary;
        }

        public void SetSalary(decimal salary)
        {
            if (salary < 0)
                throw new ArgumentException("Salary cannot be negative");

            _salary = salary;
        }

        public void AddAddress(string address)
        {
            addresses.Add(address);
        }

        public List<string> GetAddresses()
        {
            return addresses;
        }
    }

    // 3. Instructor class
    public class Instructor : Person, IInstructorService
    {
        public DateTime JoinDate { get; set; }
        public Department Department { get; set; }

        public Instructor(string name, DateTime birthDate, DateTime joinDate)
            : base(name, birthDate)
        {
            JoinDate = joinDate;
        }

        public int GetYearsOfExperience()
        {
            return DateTime.Now.Year - JoinDate.Year;
        }

        public override decimal CalculateSalary()
        {
            decimal baseSalary = base.CalculateSalary();
            int years = GetYearsOfExperience();
            decimal bonus = years * 1000;
            return baseSalary + bonus;
        }
    }

    // 4. Student class
    public class Student : Person, IStudentService
    {
        public Dictionary<Course, char> CoursesWithGrades { get; set; } = new();

        public Student(string name, DateTime birthDate)
            : base(name, birthDate) { }

        public void EnrollInCourse(Course course)
        {
            CoursesWithGrades[course] = 'F';
            course.EnrollStudent(this);
        }

        public double CalculateGPA()
        {
            if (CoursesWithGrades.Count == 0) return 0.0;

            double totalPoints = 0;
            foreach (var grade in CoursesWithGrades.Values)
            {
                totalPoints += grade switch
                {
                    'A' => 4.0,
                    'B' => 3.0,
                    'C' => 2.0,
                    'D' => 1.0,
                    _ => 0.0
                };
            }
            return totalPoints / CoursesWithGrades.Count;
        }
    }

    // 5. Course class
    public class Course : ICourseService
    {
        public string CourseName { get; set; }
        public List<Student> EnrolledStudents { get; set; } = new();

        public Course(string courseName)
        {
            CourseName = courseName;
        }

        public void EnrollStudent(Student student)
        {
            EnrolledStudents.Add(student);
        }
    }

    // 6. Department class
    public class Department : IDepartmentService
    {
        public string Name { get; set; }
        public Instructor Head { get; set; }
        public decimal Budget { get; set; }
        public List<Course> CoursesOffered { get; set; } = new();

        public Department(string name)
        {
            Name = name;
        }

        public void AddCourse(Course course)
        {
            CoursesOffered.Add(course);
        }
    }

    // Sample program
    class Program
    {
        static void Main()
        {
            Instructor prof = new("Dr. Smith", new DateTime(1980, 4, 15), new DateTime(2010, 9, 1));
            prof.SetSalary(70000);
            prof.AddAddress("123 Main St");

            Student student = new("Jane Doe", new DateTime(2002, 3, 20));
            student.SetSalary(0);  // Not used, but available from base class

            Course math = new("Math 101");
            Course cs = new("CS 101");
            student.EnrollInCourse(math);
            student.EnrollInCourse(cs);
            student.CoursesWithGrades[math] = 'A';
            student.CoursesWithGrades[cs] = 'B';

            Department compSci = new("Computer Science")
            {
                Head = prof,
                Budget = 1000000
            };
            compSci.AddCourse(math);
            compSci.AddCourse(cs);

            Console.WriteLine($"{student.Name} GPA: {student.CalculateGPA()}");
            Console.WriteLine($"{prof.Name} Salary: {prof.CalculateSalary()}");
        }
    }
}
