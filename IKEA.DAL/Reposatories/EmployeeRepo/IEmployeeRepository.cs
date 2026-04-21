using IKEA.DAL.Models.Departments;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Reposatories.GenericRepo;

namespace IKEA.DAL.Reposatories.EmployeeRepo;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    public IEnumerable<Employee> GetAll(string? searchValue);

}
