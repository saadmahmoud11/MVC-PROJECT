using Microsoft.AspNetCore.Identity;

namespace IKEA.DAL.Models.Auth;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
