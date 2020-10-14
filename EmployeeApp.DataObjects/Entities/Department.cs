using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeApp.DataObjects.Entities
{
    public class Department
    {
        /*department id, department name*/
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
