using AutoMapper;
using IKEA.BLL.Dtos.EmployeeDtos;
using IKEA.DAL.Models.Employees;

namespace IKEA.BLL.Common.MappingProfiles;

public class ProjectMapperProfile : Profile
{
    public ProjectMapperProfile()
    {
        CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.DepartmentName, options => options.MapFrom(src => src.Department != null ? src.Department.Name : "N/A"))
            .ForMember(dest => dest.DepartmentId, options => options.MapFrom(src => src.DepartmentId)).ReverseMap();
        CreateMap<Employee, EmployeeDetailsDto>()
            .ForMember(dest => dest.DepartmentName, options => options.MapFrom(src => src.Department != null ? src.Department.Name : "N/A"))
            .ForMember(dest => dest.DepartmentId, options => options.MapFrom(src => src.DepartmentId)).ReverseMap();
        CreateMap<CreatedEmployeeDto, Employee>()
            .ForMember(dest => dest.EmployeeType, options => options.MapFrom(src => src.EmpType))
            .ForMember(dest => dest.Gender, options => options.MapFrom(src => src.EmpGender));
        CreateMap<UpdatedEmployeeDto, Employee>()
                        .ForMember(dest => dest.EmployeeType, options => options.MapFrom(src => src.EmployeeType))
            .ForMember(dest => dest.Gender, options => options.MapFrom(src => src.Gender));

    }
}
