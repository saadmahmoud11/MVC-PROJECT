using IKEA.BLL.Dtos.DepartmentDtos;
using IKEA.BLL.Services.DepartmentService;
using IKEA.PL.ViewModels.DepartmentVms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers;
[Authorize]
public class DepartmentController : Controller
{
    private readonly IDepartmentService _departmentService;
    private readonly ILogger<DepartmentController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment webHostEnvironment)
    {
        _departmentService = departmentService;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
    }
    public IActionResult Index()
    {
        ViewData["Message"] = "Hello from Department Controller view data";
        ViewBag.Message = "Hello from Department Controller view bag";
        var departments = _departmentService.GetAllDepartments();
        return View(departments);
    }
    [HttpGet]
    public IActionResult Create()
        => View();
    [HttpPost]
    public IActionResult Create(DepartmentViewModel vm)
    {
        if (ModelState.IsValid)
        {
            var createdDepartmentDto = new CreatedDepartmentDto
            {
                Name = vm.Name,
                Description = vm.Description,
                Code = vm.Code
            };
            try
            {
                int result = _departmentService.AddDepartment(createdDepartmentDto);
                if (result > 0)
                {
                    TempData["Message"] = $"Department {vm.Name} created successfully.";
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to create department. Please try again.");
                    return View(vm);
                }
            }
            catch (Exception ex)
            {
                if (_webHostEnvironment.IsDevelopment())
                {
                    _logger.LogError(ex.Message);
                    return View(vm);
                }
                else
                {
                    throw;
                }
            }

        }
        else
        {
            TempData["Message"] = $"Department {vm.Name} creation failed.";
            return View(vm);
        }
    }
    [HttpGet]
    public IActionResult Details(int? id)
    {
        if (id == null)
            return BadRequest();
        var department = _departmentService.GetDepartmentById(id.Value);
        if (department is null)
            return NotFound();
        return View(department);
    }
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null)
            return BadRequest();
        var department = _departmentService.GetDepartmentById(id.Value);
        if (department is null)
            return NotFound();
        var viewDepartment = new DepartmentViewModel
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description,
            Code = department.Code
        };

        return View(viewDepartment);
    }
    [HttpPost]
    public IActionResult Edit([FromRoute] int? id, DepartmentViewModel departmentViewModel)
    {
        if (!ModelState.IsValid)
            return View(departmentViewModel);

        var updateDepartment = new UpdatedDepartmentDto
        {
            Id = id.Value,
            Name = departmentViewModel.Name,
            Description = departmentViewModel.Description,
            Code = departmentViewModel.Code
        };
        try
        {
            int result = _departmentService.UpdatedDepartment(updateDepartment);
            if (result > 0)
                return RedirectToAction(nameof(Index));
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to update department. Please try again.");
                return View(departmentViewModel);
            }
        }
        catch (Exception ex)
        {
            if (_webHostEnvironment.IsDevelopment())
            {
                _logger.LogError(ex.Message);
                return View(departmentViewModel);
            }
            else
            {
                throw;
            }
        }
    }
    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id is null)
        {
            return BadRequest();
        }
        var department = _departmentService.GetDepartmentById(id.Value);
        if (department is null)
        {
            return NotFound();
        }
        return View(department);
    }
    [HttpPost]
    public IActionResult Delete (int deptId)
    {
        var massage = string.Empty;
        try
        {
           var isDeleted =  _departmentService.DeleteDepartment(deptId);
            if (isDeleted > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            massage = "Failed to delete department. Please try again.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,ex.Message);
            if (_webHostEnvironment.IsDevelopment())
            {
                massage = ex.Message;
            }
            else
            {
                massage = "An error occurred while deleting the department. Please try again later.";
            }
        }
        ModelState.AddModelError(string.Empty, massage);
        return RedirectToAction(nameof(Delete), new { deptId });

    }

}
