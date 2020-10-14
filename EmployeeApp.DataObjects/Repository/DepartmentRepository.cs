using EmployeeApp.DataObjects.Entities;
using EmployeeApp.DataObjects.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApp.DataObjects.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _context;

        public DepartmentRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Department> CreateDepartment(Department objDepartment)
        {           
            await _context.Department.AddAsync(objDepartment);
            await _context.SaveChangesAsync();
            return objDepartment;
        }
        public async Task<bool> DepartmentExists(string departmentName)
        {
            if (await _context.Department.AnyAsync(x => x.DepartmentName.ToLower() == departmentName.ToLower()))
            {
                return true;
            }
            return false;
        }
        public async Task<Department> GetDept(int Id)
        {
            var department = await _context.Department.FirstOrDefaultAsync(d => d.DepartmentID == Id);
            return department;
        }
    }
}
