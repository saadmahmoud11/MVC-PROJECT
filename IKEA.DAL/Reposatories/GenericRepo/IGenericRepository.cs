using IKEA.DAL.Models.Employees;
using IKEA.DAL.Models.Shared;

namespace IKEA.DAL.Reposatories.GenericRepo;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    public IQueryable<TEntity> GetAll(bool withTracking = false);
    public TEntity GetById(int id);
    public void Add(TEntity item);
    public void Update(TEntity item);
    public void Delete(int? id);

}
