using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeApp.DataObjects.Entities;
using EmployeeApp.DataObjects.Interface;
using EmployeeApp.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EmployeeController(IEmployeeRepository repo, IConfiguration config, IMapper mapper, ILogger<EmployeeController> logger)
        {
            _repo = repo;
            _config = config;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] EmployeesRegDTO objEmployee)
        {
            _logger.LogInformation("Employee Register method Started");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _repo.EmployeeExists(objEmployee.EmailID.ToLower()))
            {
                _logger.LogError("Email Already Exist "+ objEmployee.EmailID);
                return BadRequest("Email Already Exist");
            }
            var userToCreate = _mapper.Map<Employee>(objEmployee);

            var createEmployee = await _repo.Register(userToCreate);
            _logger.LogInformation("Employee Register Sucessfully and Email Sent");
            var employeeToReturn = _mapper.Map<EmployeesDTO>(createEmployee);

            return CreatedAtRoute("GetEmployee", new { Controller = "Employee", id = createEmployee.EmployeeID }, employeeToReturn);
        }

        [HttpGet("{id}", Name = "GetEmployee")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var dept = await _repo.GetEmployee(id);
            var deptToReturn = _mapper.Map<EmployeesDTO>(dept);
            return Ok(deptToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, EmployeesRegDTO objEmpForUdateDto)
        {
            _logger.LogInformation("Employee Update() Initialized");
            var userFromRepo = await _repo.GetEmployee(id);

            _mapper.Map(objEmpForUdateDto, userFromRepo);

            if (await _repo.SaveAll())
            {
                _logger.LogInformation("Employee Update successfully ID : "+ userFromRepo.EmployeeID );
                return Ok(new { MsgCode = "02", TransferData = "Update Successfully" });
            }

            throw new Exception($"Updating user {id} failed on save");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var user = await _repo.GetEmployee(id);
            if (user == null)
                return BadRequest("Employee Not Exist");

            var managerList = await _repo.GetManager(id);
            if (managerList.Count > 0)
            {
                return BadRequest("Employee ID cannot able to delete due to linked with other Employee");
            }

            _repo.Delete(user);

            if (await _repo.SaveAll())
            {
                MailDTO objMail = new MailDTO();
                objMail.TO = user.EmailID;
                objMail.Subject = "Employee Deletion";
                objMail.Message = "Dear " + user.FirstName + "<br/><br/>"
                        + "User Employee Id ( "+ id + " ) is Deleted Sucessfully";
                _repo.SendEmail(objMail);
                _logger.LogInformation("Delete Employee Sucessfully and Email Sent");

                return Ok(new { MsgCode = "03", TransferData = "Delete Successfully" });
            }
            
            return BadRequest("Failed to delete the Employee");

            throw new Exception($"Delete user {id} has Dependency with Other User, Please Contact Adminisatrator");
        }
        
        //[ActionName("Status")]
        [HttpGet]
        [Route("Status/{id}")]
        public async Task<IActionResult> EmployeeStatus(int id)
        {
            var dept = await _repo.GetEmployee(id);
            var result = _repo.GetStatus(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("ListAll")]
        public List<EmployeesListDTO> EmployeeALL()
        {

            return _repo.GetEmp().ToList();
            //var deptToReturn = _mapper.Map<List<EmployeesListDTO>>(ListAll.ToList());
            //return Ok();
        }

        [HttpGet]
        [Route("EmployeeFilter")]
        public ActionResult<List<EmployeeFilterDto>> EmployeeFilterAsync([FromBody] EmployeeFilterDto objEmployee)
        {

            if (string.IsNullOrEmpty(objEmployee.EmployeeID.ToString()) &&
                string.IsNullOrEmpty(objEmployee.FirstName) &&
                string.IsNullOrEmpty(objEmployee.LastName.ToString()) &&
                string.IsNullOrEmpty(objEmployee.DepartmentName.ToString()))

                return BadRequest("Minimum one of the above field is mandatory for the filter criteria");

            return _repo.GetEmpFilter(objEmployee);

        }

        [HttpGet]
        [Route("EmpStatus/{id}")]
        public string Employeestatus(int Id)
        {

            return _repo.GetStatus(Id);
            //var deptToReturn = _mapper.Map<List<EmployeesListDTO>>(ListAll.ToList());
            //return Ok();
        }


    }
}
