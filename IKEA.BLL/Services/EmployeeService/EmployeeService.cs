using AutoMapper;
using IKEA.BLL.Common.Services.Attachments;
using IKEA.BLL.Dtos.EmployeeDtos;
using IKEA.DAL.Models.Employees;
using IKEA.DAL.Reposatories.EmployeeRepo;
using IKEA.DAL.UOW;

namespace IKEA.BLL.Services.EmployeeService;

public class EmployeeService : IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAttachmentServices _attachmentServices;

    public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper,
        IAttachmentServices attachmentServices)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _attachmentServices = attachmentServices;
    }
    public IEnumerable<EmployeeDto> GetAllEmployees()
    {
        var employees = _unitOfWork.EmployeeRepository.GetAll().ToList();
        return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
    }
    //=> _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(_repository.GetAll().Where(e => e.IsDeleted == false));


    public EmployeeDetailsDto GetEmployeeById(int id)
    {
        var employee = _unitOfWork.EmployeeRepository.GetById(id);
        var mappedEmployee = _mapper.Map<Employee, EmployeeDetailsDto>(employee);
        return mappedEmployee;
    }

    public int AddEmployee(CreatedEmployeeDto createdEmployeeDto)
    {
        var emp = _mapper.Map<CreatedEmployeeDto, Employee>(createdEmployeeDto);
        emp.CreatedBy = 1;
        emp.CreatedOn = DateTime.Now;
        emp.LastModifiedBy = 1;
        emp.LastModifiedOn = DateTime.Now;

        if (createdEmployeeDto.Image is not null)
        {
            var imageName = _attachmentServices.UploadImage(createdEmployeeDto.Image,"images");
            emp.ImageName = imageName;
        }
        _unitOfWork.EmployeeRepository.Add(emp);
        return _unitOfWork.Complete();
    }
    public int UpdatedEmployee(UpdatedEmployeeDto updatedEmployeeDto)
    {
        var emp = _mapper.Map<UpdatedEmployeeDto, Employee>(updatedEmployeeDto);
        emp.LastModifiedBy = 1;
        emp.LastModifiedOn = DateTime.Now;
        if (updatedEmployeeDto.Image is not null)
        {
            if (emp.ImageName is not null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "images", emp.ImageName);
                _attachmentServices.DeleteImage(filePath);
            }
            emp.ImageName = _attachmentServices.UploadImage(updatedEmployeeDto.Image, "images");
        }
        _unitOfWork.EmployeeRepository.Update(emp);
        return _unitOfWork.Complete();
    }
    public int DeleteEmployee(int? id)
    {
        if (id == null)
            return 0;
        var employee = _unitOfWork.EmployeeRepository.GetById(id.Value);
        if (employee is not null)
        {
            if (employee.ImageName is not null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files" ,"images", employee.ImageName);
                _attachmentServices.DeleteImage(filePath);
            }
             _unitOfWork.EmployeeRepository.Delete(employee.Id);
            if (_unitOfWork.Complete() > 0)
                return id.Value;
            else
                return 0;
        }
        else
            return 0;
    }

    public IEnumerable<EmployeeDto> GetSearchedEmployees(string? searchValue)
    {
        var employees = _unitOfWork.EmployeeRepository.GetAll(searchValue).ToList();
        return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
    }
}
