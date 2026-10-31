using exam_9_Electronic_wallet.BLL.Services.Interfaces;
using exam_9_Electronic_wallet.Models;
using exam_9_Electronic_wallet.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace exam_9_Electronic_wallet.Controllers;

public class TransactionController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IWalletService _walletService;

    public TransactionController(UserManager<User> userManager, IWalletService walletService)
    {
        _userManager = userManager;
        _walletService = walletService;
    }

    public async Task<IActionResult> History(DateTime? dateFrom, DateTime? dateTo)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Challenge();
        var transactions = await _walletService.GetTransactionsAsync(
            user.Id,
            dateFrom,
            dateTo);
        var model = new TransactionsHistoryViewModel
        {
            Transactions = transactions,
            DateFrom = dateFrom,
            DateTo = dateTo
        };

        return View(model);
    }
}