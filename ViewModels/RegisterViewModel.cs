using System.ComponentModel.DataAnnotations;

namespace RunGroupWebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Электронная почта")]
        [Required(ErrorMessage = "Электронная почта обязательна")]
        public string EmailAddress { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Подтвердите пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль не соответствует")]
        public string ConfirmPassword { get; set; }
    }

}