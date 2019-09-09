using System.ComponentModel.DataAnnotations;

namespace Diyabetiz.MVC.WebUI.Models.AccountModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Bu alanı doldurmak zorunludur")]
        [MaxLength(40, ErrorMessage = "Bu alan maksimum 40 karakter uzunluğunda olabilir")]
        [EmailAddress(ErrorMessage = "Yanlış E-Posta Formatı. Örn: hkd@hkd.com")]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bu alanı doldurmak zorunludur")]
        [MinLength(6, ErrorMessage = "Bu alan minimum 6 karakter uzunluğunda olabilir")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}