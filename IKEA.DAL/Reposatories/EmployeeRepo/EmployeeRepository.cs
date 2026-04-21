using IKEA.DAL.Contexts;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Reposatories.GenericRepo;

namespace IKEA.DAL.Reposatories.EmployeeRepo;

public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<Employee> GetAll(string? searchValue)
    {
        if (searchValue is null)
            return GetAll();
        var result = _context.Employees.Where(e => e.Name.Trim().ToLower().Contains(searchValue.Trim().ToLower()));
        return result;
    }
}
