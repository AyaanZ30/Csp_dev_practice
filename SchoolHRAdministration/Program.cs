using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using HRAdministrationAPI;

namespace SchoolHRAdministration
{
    public enum EmployeeType
    {
        Teacher,
        HeadOfDepartment,
        DeputyHeadMaster,
        HeadMaster 
    }
    class Program
    {
        public delegate decimal EarningsCalculator(int years, decimal salary);
        // Hence : EarningsCalculator x = CalculateEarnings
        static void Main(string[] args)
        {
            List<IEmployee> employees = new List<IEmployee>();
            SeedData(employees);

            ReadYearsOfExperience(employees);

            decimal totalEarnings = 0.0m;
            EarningsCalculator calc = CalculateEarnings;
            foreach(var result in EarningsPerEmployee(employees, calc))
            {
                System.Console.WriteLine($"{result.Name} earning : {result.Earning}");
                totalEarnings += result.Earning;
            }

            System.Console.WriteLine($"Total earnings: {totalEarnings}");
        }

        static decimal CalculateEarnings(int years, decimal salary)
        {
            return (years * salary);
        }
        static IEnumerable<(string Name, decimal Earning)> EarningsPerEmployee(IEnumerable<IEmployee> employees, EarningsCalculator calculator)
        {
            foreach(var e in employees)
            {
                if(e is IExperiencedEmployee ex)
                {
                    // will call CalculateEarnings(years, salary) as calculator(the delegate) = CalculateEarnings
                    yield return (e.firstName, calculator(ex.YearsOfExperience, e.salary));      
                }
            }            
        }
        public static void ReadYearsOfExperience(List<IEmployee> employees)
        {
            foreach (var employee in employees)
            {
                if (employee is IExperiencedEmployee experienced)
                {
                    Console.Write($"Enter years of experience for {employee.firstName}: ");
                    experienced.YearsOfExperience = int.Parse(Console.ReadLine());
                }
            }
        }
        public static void SeedData(List<IEmployee> employees)
        {
            // Teacher extends EmployeeBase extends IEmployee (so IEmployee teacher1 also works)
            IEmployee e1 = EmployeeFactory.GetEmployeeInstance(EmployeeType.DeputyHeadMaster, 1, "Dnyanesh", "Sawant", 10000);
            employees.Add(e1);

            IEmployee e2 = EmployeeFactory.GetEmployeeInstance(EmployeeType.HeadOfDepartment, 2, "Hitansh", "Mehta", 7000);
            employees.Add(e2);

            IEmployee e3 = EmployeeFactory.GetEmployeeInstance(EmployeeType.Teacher, 3, "Veer", "Raje", 6000);
            employees.Add(e3);

            IEmployee e4 = EmployeeFactory.GetEmployeeInstance(EmployeeType.HeadMaster, 4, "Jethalal", "Gada", 15000);
            employees.Add(e4);
        }
    }

    public class Teacher : EmployeeBase, IExperiencedEmployee
    {
        public override decimal salary{get => base.salary + (base.salary * 0.02m);} 

        public int YearsOfExperience{get; set;}  
    }

    public class HeadOfDepartment : EmployeeBase, IExperiencedEmployee
    {
        public override decimal salary{get => base.salary + (base.salary * 0.03m);}
        public int YearsOfExperience{get; set;} 
    }

    public class DeputyHeadMaster : EmployeeBase, IExperiencedEmployee
    {
        public override decimal salary{get => base.salary + (base.salary * 0.04m);}
        public int YearsOfExperience{get; set;} 
    }

    public class HeadMaster : EmployeeBase, IExperiencedEmployee
    {
        public override decimal salary{get => base.salary + (base.salary * 0.05m);}
        public int YearsOfExperience{get; set;} 
    }

    public static class EmployeeFactory
    {
        public static IEmployee GetEmployeeInstance(EmployeeType employeeType, int Id, string firstName, string lastName, decimal salary)
        {
            IEmployee employee = null;
            switch (employeeType)
            {
                case EmployeeType.Teacher:
                {
                    employee = FactoryPattern<Teacher, IEmployee>.getInstance();
                    break;
                }
                case EmployeeType.HeadOfDepartment:
                {
                    employee = FactoryPattern<HeadOfDepartment, IEmployee>.getInstance();
                    break;
                }
                case EmployeeType.DeputyHeadMaster:
                {
                    employee = FactoryPattern<DeputyHeadMaster, IEmployee>.getInstance();
                    break;
                }
                case EmployeeType.HeadMaster:
                {
                    employee = FactoryPattern<HeadMaster, IEmployee>.getInstance();
                    break;
                } 
            }
            if(employee != null)
            {
                employee.Id = Id;
                employee.firstName = firstName;
                employee.lastName = lastName;
                employee.salary = salary;
            }
            else
            {
                throw new NullReferenceException();
            }
            return employee;
        }
    }
}
