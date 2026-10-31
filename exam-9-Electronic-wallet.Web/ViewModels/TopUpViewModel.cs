using System.ComponentModel.DataAnnotations;

namespace exam_9_Electronic_wallet.ViewModels;

public class TopUpViewModel
{
    [Required]
    public string AccountNumber { get; set; } = null!;
    [Range(1, 10000000, ErrorMessage = "Введите положительную сумму")]
    public decimal Amount { get; set; }
}