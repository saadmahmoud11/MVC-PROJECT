using IKEA.DAL.Models.Auth;
using IKEA.PL.Helpers;
using IKEA.PL.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Timers;

namespace IKEA.PL.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var user = new ApplicationUser()
        {
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.UserName,
        };
        var result = _userManager.CreateAsync(user, model.Password).Result;
        if (result.Succeeded)
        {
            return RedirectToAction("Login");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var user = _userManager.FindByEmailAsync(model.Email).Result;
        if (user is not null)
        {
            bool flag = _userManager.CheckPasswordAsync(user, model.Password).Result;
            if (flag)
            {
                var result = _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false).Result;
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError(string.Empty, "You are not allowed to login");
                }
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Your account is locked out");
                }
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        ModelState.AddModelError(string.Empty, "Invalid email or password");
        return View(model);

    }
    public IActionResult SignOut()
    {
        _signInManager.SignOutAsync().GetAwaiter().GetResult();
        return RedirectToAction(nameof(Login));
    }
    [HttpGet]
    public IActionResult ForgitPassword()
    {
        return View();
    }
    [HttpPost]
    public IActionResult SendEmail(ForgitPasswordViewModel model)
    {
        if (ModelState.IsValid) // server side validation
        {
            var user = _userManager.FindByEmailAsync(model.Email).Result;
            if (user is not null)
            {
                var token = _userManager.GeneratePasswordResetTokenAsync(user).Result; // generate a token for password reset for one time use
                var passwordResetLink = Url.Action("ResetPassword", "Account", new { email = user.Email, token = token },Request.Scheme);
                //https://localhost:7283/Account/ResetPassword?email=user.Email&token=token
                var email = new Email()
                {
                    To = user.Email,
                    Subject = "Password Reset",
                    Body = $"Click the link below to reset your password:\n{passwordResetLink}"
                           
                }; // helper method to send email
                EmailSettings.SendEmail(email);
                return RedirectToAction(nameof(CheckYourInbox));
            }
            ModelState.AddModelError(string.Empty, "If the email is registered, you will receive a password reset link.");
            return View(model);
        }
        return View(model);
    }
    public IActionResult CheckYourInbox()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ResetPassword(string email, string token)
    {
        TempData["email"] = email;
        TempData["token"] = token;
        return View();
    }
    [HttpPost]
    public IActionResult ResetPassword(ResetPasswordViewModel model)
    {
        if (ModelState.IsValid)
        {
            string email = TempData["email"] as string;
            string token = TempData["token"] as string;
            var user = _userManager.FindByEmailAsync(email).Result;
            var result = _userManager.ResetPasswordAsync(user, token,model.NewPassword).Result;
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Login));
            }
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty,error.Description);
            }
        }
        return View(model);
    }

}
