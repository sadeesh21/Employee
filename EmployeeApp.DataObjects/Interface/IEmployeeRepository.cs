using EmployeeApp.DataObjects.Entities;
using EmployeeApp.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApp.DataObjects.Interface
{
    public interface IEmployeeRepository
    {
        Task<Employee> Register(Employee objEmployee);
        Task<bool> EmployeeExists(string EmailID);
        Task<Employee> GetEmployee(int id);
        Task<List<Employee>> GetManager(int id);
        Task<bool> SaveAll();
        void Delete<T>(T entity) where T : class;
        string GetStatus(int ID);
        Task<List<Employee>> GetAllEmployee();
        List<EmployeesListDTO> GetEmp();
        List<EmployeeFilterDto> GetEmpFilter(EmployeeFilterDto obj);
        void SendEmail(MailDTO obj);
    }
}
