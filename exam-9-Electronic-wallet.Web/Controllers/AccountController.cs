using exam_9_Electronic_wallet.BLL.Services.Interfaces;
using exam_9_Electronic_wallet.Models;
using exam_9_Electronic_wallet.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace exam_9_Electronic_wallet.Controllers;

public class AccountController :Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IWalletService _walletService;
    private readonly SignInManager<User> _signInManager;
    
    public AccountController(UserManager<User> userManager, IWalletService walletService, SignInManager<User> signInManager )
    {
       _userManager = userManager;
       _walletService = walletService;
       _signInManager = signInManager;
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View(new RegisterViewModel());
    }

    [HttpPost] 
    [ValidateAntiForgeryToken] 
    public async Task<IActionResult> Register(RegisterViewModel model) 
    { 
        if (!ModelState.IsValid) 
            return View(model);
        var user = new User
        {
            UserName = model.Email,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            var accountNumber = await _walletService.GenerateAccountNumberAsync();
            await _walletService.CreateWalletAsync(user.Id, accountNumber);
            await _signInManager.SignInAsync(user, false);
            TempData["AccountNumber"]=accountNumber;
            
            return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);

        return View(model);
    }
    
    
   [HttpGet]
    public IActionResult Login(string? returnUrl)
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Home", "Index");
        }
        
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginAsync(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            User? user = null;
            if (model.Login.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(model.Login);
            }
            else
            {
                user = await _userManager.FindByNameAsync(model.Login);
            }

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    user,
                    model.Password,
                    model.RememberMe,
                    false
                );

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogOffAsync()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }
    
}

