using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeManagementContext _dbContext;


        public EmployeeController(EmployeeManagementContext context)
        {
            _dbContext = context;
        }


        // GET: EmployeeController
        public ActionResult Index()
        {

            ICollection<Employee> employees = _dbContext.Employees.ToList();
            return View("Index", employees);
        }

        public ActionResult Add()
        {

            //if (Request.Form != null)
            //{
                var firstName = Request.Form["FirstName"];
                var lastName = Request.Form["LastName"];
                var city = Request.Form["City"];
                var zip = Request.Form["zip"];

                Employee employeeModel = new Employee()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    City = city,
                    ZIP = zip,
                    CreatedDate = new DateTime()
                };

                _dbContext.Add(employeeModel);
                _dbContext.SaveChanges();


              //  return View("Index");
          //  }

            return View();
        }

        public ActionResult AddSalary()
        {

            //if (Request.Form != null)
            //{
            var employeeId = Request.Form["EmployeeId"];
            var salaryDate = Request.Form["SalaryDate"];
            var amount = Request.Form["Amount"];           
            

            EmployeeSalary employeesalaryModel = new EmployeeSalary()
            {
                EmployeeId = Int32.Parse(employeeId),
                SalaryDate = DateTime.Parse(salaryDate),
                Amount = double.Parse(amount),
                CreatedDate = new DateTime()
            };

            _dbContext.Add(employeesalaryModel);
            _dbContext.SaveChanges();


            //  return View("Index");
            //  }

            return View();
        }


        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeController/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            var firstName = Request.Form["FirstName"];
            var lastName = Request.Form["LastName"];
            var city = Request.Form["City"];
            var zip = Request.Form["zip"];

            Employee employeeModel = new Employee()
            {
                FirstName = firstName,
                LastName = lastName,
                City = city,
                ZIP = zip,
                CreatedDate = new DateTime()
            };

            _dbContext.Add(employeeModel);
            _dbContext.SaveChanges();


            return View("Index");
        }



        


        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
