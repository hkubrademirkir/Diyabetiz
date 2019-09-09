using Diyabetiz.Entities.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Diyabetiz.MVC.WebUI.Models
{
    public class ContactUsVM
    {
        

        [Required(ErrorMessage = "Bu alanı doldurmak zorunludur")]
        [MaxLength(20, ErrorMessage = "Bu alan maksimum 20 karakter uzunluğunda olabilir")]
        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu alanı doldurmak zorunludur")]
        [MaxLength(40, ErrorMessage = "Bu alan maksimum 40 karakter uzunluğunda olabilir")]
        [EmailAddress(ErrorMessage = "Yanlış E-Posta Formatı. Örn: hkd@hkd.com")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "Bu alanı doldurmak zorunludur")]
        [MaxLength(200, ErrorMessage = "Bu alan maksimum 20 karakter uzunluğunda olabilir")]
        [Display(Name = "Mesajınız")]
        public string Message { get; set; }
    }
}