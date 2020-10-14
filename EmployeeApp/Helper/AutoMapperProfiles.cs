using AutoMapper;
using EmployeeApp.DataObjects.Entities;
using EmployeeApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApp.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Department, DepartmentRegDTO>().ReverseMap();
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            CreateMap<Employee, EmployeesRegDTO>().ReverseMap();
            CreateMap<Employee, EmployeesDTO>().ReverseMap();
            //CreateMap<EmployeesDTO, Employee>().ReverseMap();
        }
    }
}
