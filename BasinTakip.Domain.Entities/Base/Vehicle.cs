using BasinTakip.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Entities.Base
{
   public class Vehicle:EntityBase<int>
    {
        [Required(ErrorMessage = "* Zorunlu Alan")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "En az 3 karakter girmelisiniz")]
        [Display(Name = "Marka")]
        public string Marka { get; set; }

        [Required(ErrorMessage = "* Zorunlu Alan")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "En az 3 karakter girmelisiniz")]
        [Display(Name = "Seri")]
        public string Serial { get; set; }

        [Required(ErrorMessage = "* Zorunlu Alan")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "En az 3 karakter girmelisiniz")]
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Required(ErrorMessage = "* Zorunlu Alan")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "En az 3 karakter girmelisiniz")]
        [Display(Name = "Plaka")]
        public string Plate { get; set; }

        [Required(ErrorMessage = "* Zorunlu Alan")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "En az 3 karakter girmelisiniz")]
        [Display(Name = "Model Yılı")]
        public string ModelDate { get; set; }
    }
}
