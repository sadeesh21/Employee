using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeApp.DataObjects.Entities
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public int DepartmentID { get; set; }
        public System.Nullable<int> ManagerID { get; set; }
        public Employee Manger { get; set; }
        public Department Department { get; set; }
    }
}
