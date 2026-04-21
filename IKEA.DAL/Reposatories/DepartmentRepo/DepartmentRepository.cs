using IKEA.DAL.Contexts;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Reposatories.GenericRepo;

namespace IKEA.DAL.Reposatories.DepartmentRepo;

public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
{
    private readonly ApplicationDbContext _context;

    public DepartmentRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

}
