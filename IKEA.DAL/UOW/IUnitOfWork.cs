using IKEA.DAL.Reposatories.DepartmentRepo;
using IKEA.DAL.Reposatories.EmployeeRepo;

namespace IKEA.DAL.UOW;

public interface IUnitOfWork
{
    public IEmployeeRepository EmployeeRepository  { get; set; }
    public IDepartmentRepository DepartmentRepository { get; set; }
    public int Complete();
}
