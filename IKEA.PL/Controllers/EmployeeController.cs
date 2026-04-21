using IKEA.BLL.Dtos.EmployeeDtos;
using IKEA.BLL.Services.DepartmentService;
using IKEA.BLL.Services.EmployeeService;
using IKEA.DAL.Models.Employees;
using IKEA.PL.ViewModels.EmployeeVms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers;
[Authorize]
public class EmployeeController : Controller
{
    private readonly IEmployeeService _employeeService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ILogger<EmployeeController> _Logger { get; }


    public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger
        , IWebHostEnvironment webHostEnvironment)
    {
        _employeeService = employeeService;
        _Logger = logger;
        _webHostEnvironment = webHostEnvironment;
    }


    public IActionResult Index(string? searchValue)
    {
        if (searchValue is null)
        {
            return View(_employeeService.GetAllEmployees());
        }
        else
        {
            return View(_employeeService.GetSearchedEmployees(searchValue));
        }
    }
    [HttpGet]
    public IActionResult Create()
    {
        //ViewData["Departments"] = _departmentService.GetAllDepartments();
        return View();
    }
    [HttpPost]
    public IActionResult Create(EmployeeViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var createdEmployeeDto = new CreatedEmployeeDto
            {
                Name = viewModel.Name,
                Age = viewModel.Age,
                Address = viewModel.Address,
                Salary = viewModel.Salary,
                EmpGender = viewModel.EmpGender,
                EmpType = viewModel.EmpType,
                Phone = viewModel.Phone,
                HiringDate = viewModel.HiringDate,
                IsActive = viewModel.IsActive,
                Email = viewModel.Email,
                DepartmentId = viewModel.DepartmentId,
                Image = viewModel.Image,
            };
            try
            {
                int result = _employeeService.AddEmployee(createdEmployeeDto);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to add employee. Please try again.");
                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                if (_webHostEnvironment.IsDevelopment())
                {
                    _Logger.LogError(ex.Message);
                    return View(viewModel);
                }
                else
                {
                    throw; // Rethrow the exception to be handled by global error handling middleware
                }
            }
        }
        else
        {
            //ViewData["Departments"] = _departmentService.GetAllDepartments();
            return View(viewModel);
        }
    }
    [HttpGet]
    public ActionResult Details(int? id)
    {
        if (id == null)
            return BadRequest();
        var employee = _employeeService.GetEmployeeById(id.Value);
        if (employee == null)
            return NotFound();
        return View(employee);
    }
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null)
            return BadRequest();
        var employee = _employeeService.GetEmployeeById(id.Value);
        if (employee == null)
            return NotFound();
        var mappedEmployee = new EmployeeViewModel
        {
            Id = employee.Id,
            Name = employee.Name,
            Age = employee.Age,
            Address = employee.Address,
            Salary = employee.Salary,
            EmpGender = employee.Gender,
            EmpType = employee.EmployeeType,
            Phone = employee.Phone,
            HiringDate = employee.HiringDate,
            IsActive = employee.IsActive,
            Email = employee.Email,
            DepartmentId = employee.DepartmentId,
            ImageName = employee.ImageName,
        };
        //ViewData["Departments"] = _departmentService.GetAllDepartments();
        return View(mappedEmployee);
    }
    [HttpPost]
    public IActionResult Edit(EmployeeViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var updatedEmployeeDto = new UpdatedEmployeeDto
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Age = viewModel.Age,
                Address = viewModel.Address,
                Salary = viewModel.Salary,
                Gender = viewModel.EmpGender,
                EmployeeType = viewModel.EmpType,
                Phone = viewModel.Phone,
                HiringDate = viewModel.HiringDate,
                IsActive = viewModel.IsActive,
                Email = viewModel.Email,
                DepartmentId = viewModel.DepartmentId,
                Image = viewModel.Image,
                ImageName = viewModel.ImageName,
            };
            try
            {
                int result = _employeeService.UpdatedEmployee(updatedEmployeeDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to update employee. Please try again.");
                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                if (_webHostEnvironment.IsDevelopment())
                {
                    _Logger.LogError(ex.Message);
                    return View(viewModel);
                }
                else
                {
                    throw;
                }
            }
        }
        else
        {
            //ViewData["Departments"] = _departmentService.GetAllDepartments();
            return View(viewModel);
        }
    }
    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id == null)
            return BadRequest();
        var employee = _employeeService.GetEmployeeById(id.Value);
        if (employee == null)
            return NotFound();
        return View(employee);
    }
    [HttpPost]
    public IActionResult Delete(int empId)
    {
        var massage = string.Empty;
        try
        {
            int result = _employeeService.DeleteEmployee(empId);
            if (result > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            massage = "Failed to delete employee. Please try again.";

        }
        catch (Exception ex)
        {
            _Logger.LogError(ex, ex.Message);

            if (_webHostEnvironment.IsDevelopment())
            {
                massage = ex.Message;
            }
            else
            {
                massage = "An error occurred while trying to delete the employee. Please try again later.";
            }
        }
        ModelState.AddModelError(string.Empty, massage);
        return RedirectToAction(nameof(Index), new { empId });
    }
}
