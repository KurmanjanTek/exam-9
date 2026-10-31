using System.ComponentModel.DataAnnotations;

namespace exam_9_Electronic_wallet.ViewModels;

public class TransferViewModel
{
    [Required(ErrorMessage = "Введите номер счёта получателя")]
    [RegularExpression(@"^\d{6}$", ErrorMessage = "Номер должен состоять из 6 цифр")]
    public string RecipientAccountNumber { get; set; } = null!;

    [Range(1, 10000000, ErrorMessage = "Сумма должна быть больше нуля")]
    public decimal Amount { get; set; }
}