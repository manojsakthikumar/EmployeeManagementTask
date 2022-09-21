using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Data;
using EmployeeManagement.Models;

namespace EmployeeManagement.Views
{
    public class EmployeeSalariesController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeeSalariesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: EmployeeSalaries
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmployeeSalaries.ToListAsync());
        }

        // GET: EmployeeSalaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeSalary = await _context.EmployeeSalaries
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employeeSalary == null)
            {
                return NotFound();
            }

            return View(employeeSalary);
        }

        // GET: EmployeeSalaries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeeSalaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EmployeeId,SalaryDate,Amount,CreatedDate")] EmployeeSalary employeeSalary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeSalary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeSalary);
        }

        // GET: EmployeeSalaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeSalary = await _context.EmployeeSalaries.FindAsync(id);
            if (employeeSalary == null)
            {
                return NotFound();
            }
            return View(employeeSalary);
        }

        // POST: EmployeeSalaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EmployeeId,SalaryDate,Amount,CreatedDate")] EmployeeSalary employeeSalary)
        {
            if (id != employeeSalary.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeSalary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeSalaryExists(employeeSalary.ID))
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
            return View(employeeSalary);
        }

        // GET: EmployeeSalaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeSalary = await _context.EmployeeSalaries
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employeeSalary == null)
            {
                return NotFound();
            }

            return View(employeeSalary);
        }

        // POST: EmployeeSalaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeSalary = await _context.EmployeeSalaries.FindAsync(id);
            _context.EmployeeSalaries.Remove(employeeSalary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeSalaryExists(int id)
        {
            return _context.EmployeeSalaries.Any(e => e.ID == id);
        }
    }
}
