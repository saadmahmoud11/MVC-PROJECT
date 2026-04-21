using IKEA.DAL.Models.Shared;
using IKEA.DAL.Models.Departments;

namespace IKEA.DAL.Models.Employees;

public class Employee : BaseEntity
{
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string? Address { get; set; }
    public decimal Salary { get; set; }
    public bool IsActive { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public DateOnly HiringDate { get; set; }
    public Gender Gender { get; set; }
    public EmployeeType EmployeeType { get; set; }
    public int? DepartmentId { get; set; }
    public virtual Department? Department { get; set; }
    public string? ImageName { get; set; } 

}
