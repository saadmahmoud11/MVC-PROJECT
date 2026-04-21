using IKEA.BLL.Dtos.DepartmentDtos;
using IKEA.BLL.Dtos.EmployeeDtos;

namespace IKEA.BLL.Services.EmployeeService;

public interface IEmployeeService
{
    public IEnumerable<EmployeeDto> GetAllEmployees();
    public EmployeeDetailsDto GetEmployeeById(int id);
    public int AddEmployee(CreatedEmployeeDto createdEmployeeDto);
    public int UpdatedEmployee(UpdatedEmployeeDto updatedEmployeeDto);
    public int DeleteEmployee(int? id);
    public IEnumerable<EmployeeDto> GetSearchedEmployees(string? searchValue);

}
