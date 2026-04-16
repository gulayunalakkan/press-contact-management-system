using BasinTakip.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Entities.Base
{
    public class Firm : EntityBase<int>
    {
        [Required]
        [Display(Name = "Ad")]
        public string Name { get; set; }
    }
}
