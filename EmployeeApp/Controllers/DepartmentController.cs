using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeApp.DataObjects.Entities;
using EmployeeApp.DataObjects.Interface;
using EmployeeApp.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EmployeeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public DepartmentController(IDepartmentRepository repo, IConfiguration config, IMapper mapper, ILogger<DepartmentController> logger)
        {
            _repo = repo;
            _config = config;
            _mapper = mapper;
            _logger = logger;
        }
                
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] DepartmentRegDTO objDeparment)
        {
            _logger.LogInformation("Log message in the Register method Started");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);           

            if (await _repo.DepartmentExists(objDeparment.DepartmentName.ToLower()))
            {
                _logger.LogInformation("Log message in the Deparment Register Method already exist "+ objDeparment.DepartmentName);
                return BadRequest("Department Already Exist");

            }
            var deptToCreate = _mapper.Map<Department>(objDeparment);

            var createDepart = await _repo.CreateDepartment(deptToCreate);
            //return StatusCode(201);
            var departToReturn = _mapper.Map<DepartmentDTO>(createDepart);
            return CreatedAtRoute("GetDepartment", new { Controller = "Department", id = createDepart.DepartmentID }, departToReturn);
        }

        // GET: api/GetDeparttment/5
        [HttpGet("{id}", Name = "GetDepartment")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            _logger.LogInformation("Log message in the Get Department method Started");
            var dept = await _repo.GetDept(id);
            var deptToReturn = _mapper.Map<DepartmentDTO>(dept);
            return Ok(deptToReturn);
        }

    }
}
