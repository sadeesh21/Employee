using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeApp.DTO
{
    public class DepartmentDTO
    {

        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        //public ICollection<EmployeesDTO> Employees { get; set; }
    }
    public class DepartmentRegDTO
    {   
        public string DepartmentName { get; set; }
        
    }    
}
