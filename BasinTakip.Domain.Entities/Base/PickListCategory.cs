using BasinTakip.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Entities.Base
{
  public class PickListCategory :EntityBase<int>
    {
        [Required]
        [Display(Name = "PickList Kategori Ad")]
        public string Name { get; set; }

    }
}
