using BasinTakip.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Entities.Base
{

   public class Edition:EntityBase<int>
    {
        [Required(ErrorMessage = "* Zorunlu Alan")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "En az 3 karakter girmelisiniz")]
        [Display(Name = "Yayın Adı")]
        public string Name { get; set; }

        [Required(ErrorMessage = "* Zorunlu Alan")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "En az 3 karakter girmelisiniz")]
        [Display(Name = "Adres")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "* Zorunlu Alan")]
        public int EditionTypeId { get; set; } //picklist
    }
}
