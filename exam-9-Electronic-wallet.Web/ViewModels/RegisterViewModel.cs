using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace exam_9_Electronic_wallet.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Введите Email")] [EmailAddress(ErrorMessage = "Некорректный формат Email")] [Display(Name = "Адрес электронной почты")] public string? Email { get; set; }
    [Required(ErrorMessage = "Введите пароль")]
    [StringLength(50, MinimumLength = 6,
        ErrorMessage = "Длина пароля должна быть не менее 6 символов")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Подтвердите пароль")]
    [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    [Display(Name = "Подтвердите пароль")]
    public string? ConfirmPassword { get; set; }
}