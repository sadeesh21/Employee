using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmployeeApp.DTO
{
    public class EmployeesDTO
    {
        public int EmployeeID { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email id required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string EmailID { get; set; }
        [Required(ErrorMessage = "Deparment Id is required")]
        public int DepartmentID { get; set; }
        public int? ManagerID { get; set; }
        //public DepartmentDTO Department { get; set; }
    }
    public class EmployeesRegDTO
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email id required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [EmailAddress]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Deparment Id is required")]
        public int? DepartmentID { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? ManagerID { get; set; }
        //public DepartmentDTO Department { get; set; }
    }
    public class EmployeesListDTO
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string DepartmentName { get; set; }
        public string ManagerName { get; set; }

    }
    public class EmployeeFilterDto
    {        
        public int? EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DepartmentName { get; set; }
    }
    public class EmployeeStatuDto
    {
        public String EmployeeDesignation { get; set; }
    }
}
