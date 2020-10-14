using EmployeeApp.DataObjects.Entities;
using EmployeeApp.DataObjects.Interface;
using EmployeeApp.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApp.DataObjects.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> EmployeeExists(string EmailID)
        {
            if (await _context.Employee.AnyAsync(x => x.EmailID.ToLower() == EmailID.ToLower()))
            {
                return true;
            }
            return false;
        }

        public async Task<Employee> Register(Employee objEmployee)
        {
            await _context.Employee.AddAsync(objEmployee);
            await _context.SaveChangesAsync();
            
            MailDTO objMail = new MailDTO();
            objMail.TO = objEmployee.EmailID;
            objMail.Subject = "Employee Reigistration";
            objMail.Message = "Dear " + objEmployee.FirstName + "<br/><br/>"
                    
                    + "Register Sucessfully";
            SendEmail(objMail);
            return objEmployee;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Employee> GetEmployee(int Id)
        {
            var user = await _context.Employee.FirstOrDefaultAsync(d => d.EmployeeID == Id);
            return user;
        }
        public async Task<List<Employee>> GetAllEmployee()
        {
            return await _context.Employee.ToListAsync();
        }
        public List<EmployeesListDTO> GetEmp()
        {
            var EmpList = (from emp in _context.Employee
                           join dep in _context.Department on emp.DepartmentID equals dep.DepartmentID
                           join e1 in _context.Employee on emp.ManagerID equals e1.EmployeeID into g
                           from m in g.DefaultIfEmpty()
                           where m.ManagerID != null || m.ManagerID == null
                           select new EmployeesListDTO
                           {
                               EmployeeID = emp.EmployeeID,
                               FirstName = emp.FirstName,
                               LastName = emp.LastName,
                               EmailID = emp.EmailID,
                               DepartmentName = dep.DepartmentName,
                               ManagerName = m.FirstName
                           }).OrderBy(o => o.FirstName).ToList();

            List<EmployeesListDTO> lists = EmpList.ToList();
            return lists.ToList();
        }
        
        public List<EmployeeFilterDto> GetEmpFilter(EmployeeFilterDto obj)
        {
            var EmpList = (from emp in _context.Employee
                           join dep in _context.Department on emp.DepartmentID equals dep.DepartmentID into g
                           from m in g.DefaultIfEmpty()
                           where
                           (obj.EmployeeID > 0 ? emp.EmployeeID == obj.EmployeeID : emp.EmployeeID == emp.EmployeeID) &&
                           (!string.IsNullOrEmpty(obj.FirstName) ? emp.FirstName.Contains(obj.FirstName) : emp.FirstName == emp.FirstName) &&
                           (!string.IsNullOrEmpty(obj.LastName) ? emp.LastName.Contains(obj.LastName) : emp.LastName == emp.LastName) &&
                           (!string.IsNullOrEmpty(obj.DepartmentName) ? m.DepartmentName.Contains(obj.DepartmentName) : m.DepartmentName == m.DepartmentName)


                           //!string.IsNullOrEmpty(obj.FirstName) ? emp.FirstName == obj.FirstName : emp.FirstName == emp.FirstName
                           select new EmployeeFilterDto
                           {
                               EmployeeID = emp.EmployeeID,
                               FirstName = emp.FirstName,
                               LastName = emp.LastName,
                               //EmailID = emp.EmailID,
                               DepartmentName = m.DepartmentName,
                               //ManagerName = m.FirstName
                           }).OrderBy(o => o.FirstName).ToList();

            //List<EmployeesListDTO> lists = EmpList.ToList();
            return EmpList;
        }

        public string GetStatus(int Id)
        {
            IEnumerable<Employee> employees = _context.Employee.ToList();
            var result = _context.Employee.FirstOrDefault(e => e.EmployeeID == Id);
            var status = GetEmployeeDesignation(result, employees);
            return status.ToString();
        }

        public string GetEmployeeDesignation(Employee employee, IEnumerable<Employee> employees)
        {
            if (!employee.ManagerID.HasValue)
            {
                return "Head";
            }

            if (!employees.Any(m => m.ManagerID == employee.EmployeeID))
            {
                return "Associate";
            }
            return "Manager";
        }
        public void SendEmail(MailDTO obj)
        {
            string fromaddr = "jrkurtvonnegut@gmail.com";
            string password = "demodemo";

            MailMessage msg = new MailMessage();
            msg.Subject = obj.Subject;
            msg.From = new MailAddress("sadeeshkanna@gmail.com");
            msg.To.Add(new MailAddress(obj.TO.ToString()));
            //msg.To.Add(new MailAddress(objEmployee.EmailID));
            string body = obj.Message;
            msg.Body = body;
            msg.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential(fromaddr, password);
            smtp.Credentials = nc;
            smtp.Send(msg);
        }

        public  async Task<List<Employee>> GetManager(int id)
        {
            return await _context.Employee.Where(m => m.ManagerID == id).ToListAsync();            
        }
    }
}
