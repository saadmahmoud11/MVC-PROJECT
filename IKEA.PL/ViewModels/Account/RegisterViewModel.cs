using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModels.Account;

public class RegisterViewModel
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }
    [Required]
    [MaxLength(50)]
    public string UserName { get; set; } // Unique 
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }
    [Required(ErrorMessage = "Confirm Password is required.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
    public bool IsAgree { get; set; }
}
