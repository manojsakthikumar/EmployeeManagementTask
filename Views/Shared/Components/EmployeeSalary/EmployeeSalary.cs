using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models;
using EmployeeManagement.Data;
using Microsoft.EntityFrameworkCore;


namespace EmployeeManagement.Views.Shared.Component
{
    [ViewComponent(Name = "EmployeeSalary")]
    public class EmployeeSalaryComponent : ViewComponent
    {
        public Employee employee;
        public EmployeeSalary employeesalary;
        private readonly EmployeeContext _context;


        public class EmployeeSalaryModel
        {
            public string error;
        }

        public EmployeeSalaryComponent(EmployeeContext context)
        {
            employeesalary = new EmployeeSalary();
            _context = context;
        }

        public IViewComponentResult Invoke(int employeeID)
        {
            var year = DateTime.Now.Year;
            //var employeedata =  _context.Employees.FindAsync(employeeID);
            var employeesaldata = _context.EmployeeSalaries.Where(a => a.EmployeeId == employeeID && a.SalaryDate.Year == year).ToList();



            // employee = (Employee)employeedata;
            return View("~/Views/Shared/Components/EmployeeSalary/Default.cshtml", employeesaldata);
        }



    }
}
