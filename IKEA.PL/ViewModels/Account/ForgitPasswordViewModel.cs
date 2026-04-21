using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModels.Account;

public class ForgitPasswordViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage ="Email is invalid")]
    public string Email { get; set; }
}
