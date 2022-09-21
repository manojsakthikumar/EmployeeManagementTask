using EmployeeManagement.Data;
using EmployeeManagement.Models;
using System;
using System.Linq;

namespace EmployeeManagement.Data
{
    public static class DbInitializer
    {
        public static void Initialize(EmployeeContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Employees.Any())
            {
                return;   // DB has been seeded
            }

            var employees = new Employee[]
            {
            new Employee{FirstName="Carson",LastName="Alexander",City="Chennai",ZIP="600110",CreatedDate=DateTime.Parse("2005-09-01")},
            new Employee{FirstName="Manoj",LastName="Jeyachandran",City="Regina",ZIP="S4S3C2",CreatedDate=DateTime.Parse("2006-09-01")},
            new Employee{FirstName="Test",LastName="Tesar",City="Chennai",ZIP="S4P54S",CreatedDate=DateTime.Parse("2007-09-01")}
            };
            foreach (Employee e in employees)
            {
                context.Employees.Add(e);
            }
            context.SaveChanges();

            var employeeSalaries = new EmployeeSalary[]
            {
            new EmployeeSalary{EmployeeId=1,SalaryDate=DateTime.Parse("2020-01-01"),Amount=10500.00,CreatedDate=DateTime.Now},
            new EmployeeSalary{EmployeeId=1,SalaryDate=DateTime.Parse("2020-02-01"),Amount=10500.00,CreatedDate=DateTime.Now},
            new EmployeeSalary{EmployeeId=1,SalaryDate=DateTime.Parse("2020-03-01"),Amount=10500.00,CreatedDate=DateTime.Now},
            new EmployeeSalary{EmployeeId=1,SalaryDate=DateTime.Parse("2020-04-01"),Amount=10500.00,CreatedDate=DateTime.Now},
            };
            foreach (EmployeeSalary es in employeeSalaries)
            {
                context.EmployeeSalaries.Add(es);
            }
            context.SaveChanges();

        }
    }
}