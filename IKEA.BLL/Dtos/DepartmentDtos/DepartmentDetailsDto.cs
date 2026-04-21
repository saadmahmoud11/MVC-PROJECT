using IKEA.DAL.Models.Departments;

namespace IKEA.BLL.Dtos.DepartmentDtos;

public class DepartmentDetailsDto
{
    public DepartmentDetailsDto(Department department)
    {
        Id = department.Id;
        Name = department.Name;
        Description = department.Description;
        Code = department.Code;
        CreatedBy = department.CreatedBy;
        CreatedOn = DateOnly.FromDateTime(department.CreatedOn);
        LastModifiedBy = department.LastModifiedBy;
        LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedOn);
    }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Code { get; set; }
    public int Id { get; set; }
    public int CreatedBy { get; set; }
    public DateOnly CreatedOn { get; set; }
    public int LastModifiedBy { get; set; }
    public DateOnly LastModifiedOn { get; set; }
}
