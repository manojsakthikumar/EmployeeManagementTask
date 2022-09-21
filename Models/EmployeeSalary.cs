using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class EmployeeSalary
    {
        public int ID { get; set; }
        public int EmployeeId { get; set; }
        public DateTime SalaryDate { get; set; }
        public double Amount { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;


    }
}
