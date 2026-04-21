using IKEA.BLL.Dtos.DepartmentDtos;
using IKEA.DAL.Models.Departments;
using System.Runtime.CompilerServices;

namespace IKEA.BLL.Factories.DepartmentFactory;

public static class DepartmentFact
{
    public static DepartmentDto ToDepartmentDto(this Department department)
    {
        return new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description,
            Code = department.Code
        };
    }
    public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
    {
        return new DepartmentDetailsDto(department);
      
    }
    public static Department ToDepartment(this CreatedDepartmentDto createdDepartmentDto)
    {
        return new Department
        {
            Name = createdDepartmentDto.Name,
            Description = createdDepartmentDto.Description,
            Code = createdDepartmentDto.Code,
            CreatedBy = 1,
            LastModifiedBy = 1,
            CreatedOn = DateTime.UtcNow,
            LastModifiedOn = DateTime.UtcNow,
            IsDeleted = false
        };
    }
    public static Department FromUpdatedDepartment(this UpdatedDepartmentDto updatedDepartmentDto)
        {
            return new Department
            {
                Id = updatedDepartmentDto.Id,
                Name = updatedDepartmentDto.Name,
                Description = updatedDepartmentDto.Description,
                Code = updatedDepartmentDto.Code,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow
            };
    }
}
