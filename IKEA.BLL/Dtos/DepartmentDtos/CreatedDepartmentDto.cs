using System.ComponentModel.DataAnnotations;

namespace IKEA.BLL.Dtos.DepartmentDtos;

public class CreatedDepartmentDto
{
    [Required(ErrorMessage ="name is required")]
    public string Name { get; set; }
    public string? Description { get; set; }
    [Required(ErrorMessage = "code is required")]
    public string Code { get; set; }
}
