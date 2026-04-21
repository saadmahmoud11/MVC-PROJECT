using IKEA.DAL.Contexts;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Models.Shared;

namespace IKEA.DAL.Reposatories.GenericRepo;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public IQueryable<TEntity> GetAll(bool withTracking = false)
    {
        if (withTracking)
            return _context.Set<TEntity>().Where(e => e.IsDeleted == false);
        else
            return _context.Set<TEntity>().Where(e => e.IsDeleted == false).AsNoTracking();
    }

    public TEntity GetById(int id)
    {
        var entity = _context.Set<TEntity>().Find(id);
        return entity;
    }
    public void Add(TEntity item)
    {
        _context.Set<TEntity>().Add(item);
    }
    public void Update(TEntity item)
    {
        _context.Set<TEntity>().Update(item);
    }
    public void Delete(int? id)
    {
        var entity = _context.Set<TEntity>().Find(id);
        entity.IsDeleted = true;
        _context.Set<TEntity>().Update(entity);
    }


}
