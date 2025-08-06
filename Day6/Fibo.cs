// Part 2: Object-Oriented Design with OOP Principles
using System;
using System.Collections.Generic;

namespace UniversitySystem
{
    // Abstraction: Base class
    public abstract class Person
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        protected List<string> Addresses = new List<string>();

        // Encapsulation: Private Salary field with validation
        private decimal salary;
        public decimal Salary
        {
            get => salary;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Salary cannot be negative");
                salary = value;
            }
        }

        public void AddAddress(string address)
        {
            Addresses.Add(address);
        }

        public List<string> GetAddresses()
        {
            return new List<string>(Addresses);
        }

        public int GetAge()
        {
            return DateTime.Now.Year - BirthDate.Year;
        }

        // Polymorphism: Virtual method for salary calculation
        public virtual decimal CalculateSalary()
        {
            return Salary;
        }
    }

    public class Student : Person
    {
        public string Major { get; set; }

        public override decimal CalculateSalary()
        {
            return 0; // Students usually don't have salary in this model
        }
    }

    public class Instructor : Person
    {
        public int TeachingHours { get; set; }

        public override decimal CalculateSalary()
        {
            return Salary + (TeachingHours * 50); // $50 per hour bonus
        }
    }

    // Interfaces
    public interface IPersonService
    {
        int GetAge();
        List<string> GetAddresses();
        decimal CalculateSalary();
    }

    public interface IStudentService : IPersonService
    {
        string GetMajor();
    }

    public interface IInstructorService : IPersonService
    {
        int GetTeachingHours();
    }

    public interface IDepartmentService
    {
        void AddPerson(Person person);
        List<Person> GetPeople();
    }

    // Sample class using interface
    public class Department : IDepartmentService
    {
        private List<Person> people = new List<Person>();

        public void AddPerson(Person person)
        {
            people.Add(person);
        }

        public List<Person> GetPeople()
        {
            return people;
        }
    }
}