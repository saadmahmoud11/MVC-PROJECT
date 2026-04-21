using IKEA.BLL.Dtos.DepartmentDtos;
using IKEA.BLL.Factories.DepartmentFactory;
using IKEA.DAL.Reposatories.DepartmentRepo;
using IKEA.DAL.UOW;

namespace IKEA.BLL.Services.DepartmentService;

public class DepartmentService : IDepartmentService
{
    private readonly IUnitOfWork _unitOfWork;

    public DepartmentService( IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IEnumerable<DepartmentDto> GetAllDepartments()
    {
        var departments = _unitOfWork.DepartmentRepository.GetAll();
        //var mappedDepartments = departments.Select(d => new DepartmentDto
        //{
        //    Id = d.Id,
        //    Name = d.Name,
        //    Description = d.Description,
        //    Code = d.Code
        //});
        List<DepartmentDto> mappedDepartments = new List<DepartmentDto>();
        foreach (var dept in departments)
        {
            var mappedDepartment = dept.ToDepartmentDto();
            mappedDepartments.Add(mappedDepartment);
        }
        return mappedDepartments;
    }
    public DepartmentDetailsDto GetDepartmentById(int id)
    {
        var department = _unitOfWork.DepartmentRepository.GetById(id);
        if (department is null)
        {
            return null;
        }
        else
        {
            var mappedDepartment = department.ToDepartmentDetailsDto();

            return mappedDepartment;
        }

    }
    public int AddDepartment(CreatedDepartmentDto createdDepartmentDto)
    {
        var department = createdDepartmentDto.ToDepartment();
        _unitOfWork.DepartmentRepository.Add(department);
        return _unitOfWork.Complete();
    }
    public int UpdatedDepartment(UpdatedDepartmentDto updatedDepartmentDto)
    {
        var department = updatedDepartmentDto.FromUpdatedDepartment();
        _unitOfWork.DepartmentRepository.Update(department);
        return _unitOfWork.Complete();
    }
    public int DeleteDepartment(int? id)
    {
        if (id is null)
            return 0;
        _unitOfWork.DepartmentRepository.Delete(id.Value);
        return _unitOfWork.Complete();
    }
}
