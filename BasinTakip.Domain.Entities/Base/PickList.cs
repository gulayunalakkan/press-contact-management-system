using BasinTakip.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Entities.Base
{
    public class PickList:EntityBase<int>
    {
        [Required(ErrorMessage = "* Zorunlu Alan")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "En az 3 karakter girmelisiniz")]
        [Display(Name = "PickList Ad")]
        public string Name { get; set; }
        public int? CategoryId { get; set; }
    }
}
