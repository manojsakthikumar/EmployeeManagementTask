using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using EmployeeManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;
using Serilog;


namespace EmployeeManagement.Views.Shared.Component
{
    [ViewComponent(Name = "EmployeeSalary")]
    public class EmployeeSalaryComponent : ViewComponent
    {
        public Employee employee;
        public EmployeeSalary employeesalary;
        private readonly EmployeeContext _context;

        private readonly ILogger<EmployeeSalaryComponent> logger;

        public class EmployeeSalaryModel
        {
            public string error;
        }

        public EmployeeSalaryComponent(EmployeeContext context, ILogger<EmployeeSalaryComponent> logger)
        {
            employeesalary = new EmployeeSalary();
            employee = new Employee();
            _context = context;
            this.logger = logger;
        }

        public IViewComponentResult Invoke(int employeeID)
        {
            try
            {
                var year = DateTime.Now.Year;
                //var employeedata =  _context.Employees.FindAsync(employeeID);
                //var employeesaldata = _context.EmployeeSalaries.Where(a => a.EmployeeId == employeeID && a.SalaryDate.Year == year).ToList();


                var employeesaldata = _context.EmployeeSalaries.Where
                    (a => a.EmployeeId == employeeID && a.SalaryDate.Year == year)
                    .OrderByDescending(a => a.SalaryDate).ToList();


                // employee = (Employee)employeedata;
                logger.LogInformation("Employee Salary details fetched for Employee Id :{0} and sorted ", employeeID);
                return View("~/Views/Shared/Components/EmployeeSalary/Default.cshtml", employeesaldata);
            }
            catch(Exception ex)
            {
                logger.LogError("Exception while fetching employee salary details in Invoke function",ex);
                return null;
             }
        }



    }
}
