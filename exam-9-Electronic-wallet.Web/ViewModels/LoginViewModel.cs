using System.ComponentModel.DataAnnotations;

namespace exam_9_Electronic_wallet.ViewModels;

public class LoginViewModel
{
    [Required]
    public string Login { get; set; }
    [Required(ErrorMessage = "Введите пароль")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
    [Display(Name = "Запомнить меня")]
    public bool RememberMe { get; set; }
    public string? ReturnUrl { get; set; }
}