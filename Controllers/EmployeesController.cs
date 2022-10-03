using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;

namespace EmployeeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly ILogger<EmployeesController> logger;

        public EmployeesController(EmployeeContext context,ILogger<EmployeesController> logger)
        {
            _context = context;
            this.logger = logger;
        }
        //public EmployeesController(ILogger<EmployeesController> logger)
        //{
        //    this.logger = logger;
        //}

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            try
            {
                logger.LogInformation("Called Employees List page");
                return View(await _context.Employees.ToListAsync());
            }
            catch(Exception ex)
            {
                logger.LogError(ex,"Error while calling Employees List Page");
                return NotFound();
            }
            
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var employee = await _context.Employees
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (employee == null)
                {
                    logger.LogInformation("Employee Details Not Found for Employee :{0}", id);
                    return NotFound();
                }
                logger.LogInformation("Called Employee Details for Employee :{0}",id);
                return View(employee);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error while opening Employee Details for Employee :{0}", id);
                return NotFound();
            }           

        }

        public async Task<IActionResult> EmployeeDetails(int? id)
        {
            try
            {
                if (id == null)
                {
                    logger.LogWarning("Employee Details not returned as id is null");
                    return NotFound();
                }

                var employee = await _context.Employees
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (employee == null)
                {
                    logger.LogInformation("Employee Details Not Found for Employee Id:{0}", id);
                    return NotFound();
                }
                logger.LogInformation("Called Employee Details for Employee Id:{0}", id);

                return PartialView(employee);
            }
            catch(Exception ex)
            {

                logger.LogError(ex, "Exception while returning Partial View for Employee Id : {0}", id);
                return NotFound();
            }

            
        }

        public async Task<IActionResult> EmployeeSalaryDetails(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                //Condition sort by descending added MJ Oct/02
                var employeesalary = await _context.EmployeeSalaries
                    .OrderByDescending(x=>x.SalaryDate)
                    .Where(e=>e.ID == id)
                    .FirstOrDefaultAsync(m => m.EmployeeId == id);

                if (employeesalary == null)
                {
                    logger.LogInformation("Employee Salary Details Not Found for Employee Id:{0}", id);
                    return NotFound();
                }
                logger.LogInformation("Employee Salary Details Collected for Employee Id:{0}", id);
                return PartialView(employeesalary);

            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Exception while returning Employee Salary Details for Employee Id : {0}", id);
                return NotFound();
            }
            
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,City,ZIP")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(employee);
                    await _context.SaveChangesAsync();
                    logger.LogInformation("Employee Created Successfully");
                    return RedirectToAction(nameof(Index));
                }
                logger.LogInformation("Employee Created Successfully");
                return View(employee);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Exception while creating Employee ");
                return NotFound();
            }
            
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    logger.LogWarning("Employee Details are not edited as it is returned null");
                    return NotFound();
                }

                var employee = await _context.Employees.FindAsync(id);
                if (employee == null)
                {
                    logger.LogWarning("Employee details not found for Edit");
                    return NotFound();
                }
                logger.LogInformation("Employee Details for Employee Id :{0} Edited Successfully",id);
                return View(employee);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Exception while editing Employee id:{0} ",id);
                return NotFound();
            }
            
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,City,ZIP,CreatedDate")] Employee employee)
        {
            if (id != employee.ID)
            {
                logger.LogWarning("Employee id : {0} not found in Employee Database", id);
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                    logger.LogInformation("Employee Update success for Employee id : {0}");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.ID))
                    {
                        logger.LogWarning("Employee id : {0} not found in Employee Database", employee.ID);
                        return NotFound();
                    }
                    else
                    {
                        logger.LogError("Employee Id : {0} database update Failed",id);
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }


        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /* [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> EditSave(Employee employee)
         {
             if (ModelState.IsValid)
             {
                 try
                 {

                     _context.Employees.Where(a => a.ID == employee.ID);
                     _context.Employees.Update(employee);
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!EmployeeExists(employee.ID))
                     {
                         return NotFound();
                     }
                     else
                     {
                         throw;
                     }
                 }
                 return RedirectToAction(nameof(Index));
             }
             return View(employee);
         }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSave(Employee employee)
        {
            try
            {
                //Edit Employee
                var entity = _context.Employees.FirstOrDefault(a => a.ID == employee.ID);

                if (entity != null)
                {
                    entity.FirstName = employee.FirstName;
                    entity.LastName = employee.LastName;
                    entity.ZIP = employee.ZIP;
                    entity.City = employee.City;
                    _context.Update(entity);
                    await _context.SaveChangesAsync();
                    logger.LogInformation("Employee Edit Success for Employee :{0} , {1} ", employee.LastName, employee.FirstName);
                }

                return Json(employee);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Exception while editing Employee :{0} , {1} ", employee.LastName, employee.FirstName);
                throw ex;
            }

            
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    logger.LogWarning("Employee delete returned null for Employee id : {0}", id);
                    return NotFound();
                }

                var employee = await _context.Employees
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (employee == null)
                {
                    logger.LogWarning("Employee search returned null for Employee id : {0}", id);
                    return NotFound();
                }
                logger.LogInformation("Employee Delete success for Employee id :{0}",id);
                return View(employee);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Exception while deleting Employee id:{0}", id);
                throw ex;
            }
            
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                logger.LogInformation("Employee Delete success for Employee id :{0}", id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                logger.LogError(ex,"Employee Delete failed for Employee id :{0}", id);
                return NotFound();
            }
        }

        private bool EmployeeExists(int id)
        {
            logger.LogInformation("Employee Details exists for employee id : {0}",id);
            return _context.Employees.Any(e => e.ID == id);
        }

        public async Task<IActionResult> GetEmployeeModal(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           
            return ViewComponent("EditEmployee", new { employeeID = id});
        }

        public async Task<IActionResult> GetSalaryModal(int? id)
        {
            if (id == null)
            {
                logger.LogWarning("Employee Salary details not found for empployee as it returned null");
                return NotFound();
            }

            logger.LogInformation("Employee Salary details Dialog displayed for empployee id :{0}", id);
            return ViewComponent("EmployeeSalary", new { employeeID = id });
        }

        public IActionResult CreateEmployeeSalary(int id)
        {
            EmployeeSalary empsal = new EmployeeSalary();
            empsal.EmployeeId = id;
            logger.LogInformation("Employee Salary page returned for Employee id :{0}", id);
            return View(empsal);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmployeeSalary([Bind("EmployeeId,SalaryDate,Amount,CreatedDate")] EmployeeSalary employeeSalary)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(employeeSalary);
                    await _context.SaveChangesAsync();
                    logger.LogInformation("Employee Salary created successfully for Employee Id:{0}, Salary Date :{1}",employeeSalary.EmployeeId,employeeSalary.SalaryDate);
                    return RedirectToAction(nameof(Index));
                }
                logger.LogWarning("Employee Salary State Invalid");
                return View(employeeSalary);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Exception while Creating Salary for Employee Id:{0}, SalaryDate:{1}", employeeSalary.EmployeeId,employeeSalary.SalaryDate);
                throw ex;
            }
            
        }

        public IActionResult GetEmplModal()
        {
            Employee emp = new Employee();
            logger.LogInformation("Get Employee Modal called");
            return PartialView("_EmployeeModelPartial", emp);
        }


               
        //public IActionResult SaveEdit(int id)
        //{
        //    var employeedata = _context.Employees.Where(a => a.ID == id).FirstOrDefault();
        //    Employee empclass = new Employee();
        //    empclass.ID = Convert.ToInt32(employeedata.ID);
        //    empclass.FirstName = employeedata.FirstName;
        //    empclass.LastName = employeedata.LastName;
        //    empclass.ZIP = employeedata.ZIP;
        //    logger.LogInformation("SaveEdit function called in EmployeeController");
        //    return PartialView(empclass);

        //}





        //[HttpGet]
        //public PartialViewResult Edit(Int32 employeeID)
        //{
        //    var employeedata = _context.Employees.Where(a => a.ID == employeeID).FirstOrDefault();
        //    Employee empclass = new Employee();
        //    empclass.ID = Convert.ToInt32(employeedata.ID);
        //    empclass.FirstName = employeedata.FirstName;
        //    empclass.LastName = employeedata.LastName;
        //    empclass.ZIP = employeedata.ZIP;
        //    logger.LogInformation("Edit Function called in EmployeeController");
        //    return PartialView(empclass);
        //}
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.ExceptionPath = exceptionDetails.Path;
            ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            ViewBag.Stacktrace = exceptionDetails.Error.StackTrace;

            logger.LogError("Error Page routed and Error displayed to the User");
            return View("Error");

        }


    }
}
