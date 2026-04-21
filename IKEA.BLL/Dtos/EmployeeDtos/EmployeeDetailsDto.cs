using IKEA.DAL.Models.Employees;
using IKEA.DAL.Models.Shared;

namespace IKEA.BLL.Dtos.EmployeeDtos;

public class EmployeeDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? Age { get; set; }
    public string? Address { get; set; }
    public decimal Salary { get; set; }
    public bool IsActive { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public DateOnly HiringDate { get; set; }
    public Gender Gender { get; set; }
    public EmployeeType EmployeeType { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public int LastModifiedBy { get; set; }
    public DateTime LastModifiedOn { get; set; }
    public string? DepartmentName { get; set; }
    public int? DepartmentId { get; set; }
    public string? ImageName { get; set; }
}
