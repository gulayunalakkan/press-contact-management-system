using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasinTakip.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Kullanıcı adı bilgisi boş geçilemez"), AllowHtml]
        [Display(Name = "Kullanıcı")]
        public string UserName { get; set; }

        [Required (ErrorMessage ="Şifre bilgisi boş geçilemez")]
        [AllowHtml]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Display(Name = "Beni hatırla")]
        public bool RememberMe { get; set; }

        public string ReturUrl { get; set; }

        [Display(Name = "Doğrulama Kodu")]
        [Required(ErrorMessage = "Lütfen doğrulama kodunu boş bırakmayınız.")]
        public string Captcha { get; set; }
    }
}