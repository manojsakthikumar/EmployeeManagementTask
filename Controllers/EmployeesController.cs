using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public async Task<IActionResult> EmployeeDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return PartialView(employee);
        }

        public async Task<IActionResult> EmployeeSalaryDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeesalary = await _context.EmployeeSalaries
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employeesalary == null)
            {
                return NotFound();
            }

            return PartialView(employeesalary);
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
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
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
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
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

            var entity = _context.Employees.FirstOrDefault(a => a.ID == employee.ID);

            if (entity != null)
            {
                entity.FirstName = employee.FirstName;
                entity.LastName = employee.LastName;
                entity.ZIP = employee.ZIP;
                entity.City = employee.City;
                _context.Update(entity);
                await _context.SaveChangesAsync();
            }

            return Json(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
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
                return NotFound();
            }


            return ViewComponent("EmployeeSalary", new { employeeID = id });
        }

        public IActionResult CreateEmployeeSalary(int id)
        {
            EmployeeSalary empsal = new EmployeeSalary();
            empsal.EmployeeId = id;
            return View(empsal);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmployeeSalary([Bind("EmployeeId,SalaryDate,Amount,CreatedDate")] EmployeeSalary employeeSalary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeSalary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeSalary);
        }

        public IActionResult GetEmplModal()
        {
            Employee emp = new Employee();
            return PartialView("_EmployeeModelPartial", emp);
        }


               
        public IActionResult SaveEdit(int id)
        {
            var employeedata = _context.Employees.Where(a => a.ID == id).FirstOrDefault();
            Employee empclass = new Employee();
            empclass.ID = Convert.ToInt32(employeedata.ID);
            empclass.FirstName = employeedata.FirstName;
            empclass.LastName = employeedata.LastName;
            empclass.ZIP = employeedata.ZIP;
            return PartialView(empclass);

        }





        [HttpGet]
        public PartialViewResult Edit(Int32 employeeID)
        {
            var employeedata = _context.Employees.Where(a => a.ID == employeeID).FirstOrDefault();
            Employee empclass = new Employee();
            empclass.ID = Convert.ToInt32(employeedata.ID);
            empclass.FirstName = employeedata.FirstName;
            empclass.LastName = employeedata.LastName;
            empclass.ZIP = employeedata.ZIP;
            return PartialView(empclass);
        }

    }
}
