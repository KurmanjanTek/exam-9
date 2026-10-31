using exam_9_Electronic_wallet.BLL.Services.Interfaces;
using exam_9_Electronic_wallet.Models;
using exam_9_Electronic_wallet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace exam_9_Electronic_wallet.Controllers;

public class HomeController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IWalletService _walletService;
    
    public HomeController(UserManager<User> userManager, IWalletService walletService)
    {
        _userManager = userManager;
        _walletService = walletService;
    }

    public async Task<IActionResult> Index()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var wallet = await _walletService.GetByUserIdAsync(user.Id);

                ViewBag.AccountNumber = wallet?.AccountNumber;
                ViewBag.Balance = wallet?.Balance;
            }
        }

        ViewBag.NewAccountNumber = TempData["AccountNumber"];
        return View();
    }
    
    [HttpPost]
    [AllowAnonymous] 
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TopUp(TopUpViewModel model) 
    { 
        if (!ModelState.IsValid) 
            return Json(new
            {
                success = false, 
                message = "Проверьте введённые данные"
            });
        var result = await _walletService.TopUpAsync(
            model.AccountNumber,
            model.Amount);

        if (!result)
            return Json(new
            {
                success = false,
                message = "Счёт не найден или сумма указана неверно"
            });

        return Json(new
        {
            success = true, 
            message = "Счёт успешно пополнен!"
        });
    }
    
    [HttpPost] 
    [Authorize] 
    [ValidateAntiForgeryToken] 
    public async Task<IActionResult> Transfer(TransferViewModel model) 
    {
        if (!ModelState.IsValid) 
        {
            return Json(new
            {
                success = false,
                message = "Проверьте введённые данные"
                
            }); 
        }
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Json(new
            {
                success = false,
                message = "Пользователь не найден"
            });
        }

        var result = await _walletService.TransferAsync(
            user.Id,
            model.RecipientAccountNumber,
            model.Amount);

        if (!result)
        {
            return Json(new
            {
                success = false,
                message = "Не удалось выполнить перевод. Проверьте счёт и баланс."
            });
        }

        return Json(new
        {
            success = true,
            message = "Перевод успешно выполнен!"
        });
    }
}