using System.ComponentModel.DataAnnotations;

namespace RunGroupWebApp.ViewModels
{
    public class LoginViewModel
    {    
        [Display(Name = "Электронная почта")]
        [Required(ErrorMessage = "Электронная почта обязательна")]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}