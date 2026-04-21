using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModels.Account;

public class ResetPasswordViewModel
{
    [Required(ErrorMessage = "New Password is required.")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }
    [Required(ErrorMessage = "Confirm Password is required.")]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}
