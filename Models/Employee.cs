using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string ZIP { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ICollection<EmployeeSalary> employeeSalaries { get; set; }

        public static explicit operator Employee(ValueTask<Employee> v)
        {
            throw new NotImplementedException();
        }
    }
}
