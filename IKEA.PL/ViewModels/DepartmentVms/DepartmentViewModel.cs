using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModels.DepartmentVms;

public class DepartmentViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }
    public string? Description { get; set; }
    [Required(ErrorMessage = "Code is required.")]
    public string Code { get; set; }
}

