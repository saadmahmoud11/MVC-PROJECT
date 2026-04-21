using IKEA.DAL.Contexts;
using IKEA.DAL.Reposatories.DepartmentRepo;
using IKEA.DAL.Reposatories.EmployeeRepo;

namespace IKEA.DAL.UOW;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        EmployeeRepository = new EmployeeRepository(_dbContext);
        DepartmentRepository = new DepartmentRepository(_dbContext);
    }
    public IEmployeeRepository EmployeeRepository { get; set; }
    public IDepartmentRepository DepartmentRepository { get; set; } 

    public int Complete()
    {
        return _dbContext.SaveChanges();
    }
}
