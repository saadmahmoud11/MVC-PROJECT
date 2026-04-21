using IKEA.BLL.Dtos.DepartmentDtos;

namespace IKEA.BLL.Services.DepartmentService;

public interface IDepartmentService
{
    public IEnumerable<DepartmentDto> GetAllDepartments();
    public DepartmentDetailsDto GetDepartmentById(int id);
    public int AddDepartment(CreatedDepartmentDto createdDepartmentDto);
    public int UpdatedDepartment(UpdatedDepartmentDto updatedDepartmentDto);
    public int DeleteDepartment(int? id);
}
