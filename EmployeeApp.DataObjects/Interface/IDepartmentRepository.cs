using EmployeeApp.DataObjects.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApp.DataObjects.Interface
{
    public interface IDepartmentRepository
    {
        Task<Department> CreateDepartment(Department objDepartment);
        Task<bool> DepartmentExists(string departmentName);
        Task<Department> GetDept(int id);
    }
}
