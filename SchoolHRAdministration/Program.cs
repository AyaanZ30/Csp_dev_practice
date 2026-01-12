using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using HRAdministrationAPI;

namespace SchoolHRAdministration
{
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
            Teacher e1 = new Teacher
            {
                Id = 1,
                firstName = "Kelly",
                lastName = "Brook",
                salary = 4000
            };

            employees.Add(e1);

            HeadOfDepartment e2 = new HeadOfDepartment
            {
                Id = 2,
                firstName = "Shawn",
                lastName = "Kennedy",
                salary = 5000
            };
            employees.Add(e2);

            DeputyHeadMaster e3 = new DeputyHeadMaster
            {
                Id = 3,
                firstName = "James",
                lastName = "Watson",
                salary = 8000
            };
            employees.Add(e3);

            HeadMaster e4 = new HeadMaster
            {
                Id = 4,
                firstName = "Alex",
                lastName = "Richards",
                salary = 10000
            };
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
}
