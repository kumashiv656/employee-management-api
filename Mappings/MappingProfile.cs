using AutoMapper;
using EmployeeApi.Models;
using EmployeeApi.DTOs;

namespace EmployeeApi.Mappings
{
    public class MappingProife : Profile
    {
        public MappingProife()
        {
            CreateMap< Employee, EmployeeReadDto>();
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeUpdateDto,Employee>();
            

        }
    }
}