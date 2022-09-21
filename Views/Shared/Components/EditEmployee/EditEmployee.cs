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
    [ViewComponent(Name ="EditEmployee")]
    public class EditEmployeeComponent  : ViewComponent
    {
        public Employee employee;
        private readonly EmployeeContext _context;     


        public class EditEmployeeModel
        {
            public string error;
        }

        public EditEmployeeComponent(EmployeeContext context)
        {
            employee = new Employee();
            _context = context;
        }

        public IViewComponentResult Invoke(int employeeID)
        {

            //var employeedata =  _context.Employees.FindAsync(employeeID);
            var employeesalarydata =  _context.Employees.Where(a => a.ID == employeeID).ToList();

            

           // employee = (Employee)employeedata;
            return View("~/Views/Shared/Components/EditEmployee/Default.cshtml", employeesalarydata);
        }
       


    }
}
