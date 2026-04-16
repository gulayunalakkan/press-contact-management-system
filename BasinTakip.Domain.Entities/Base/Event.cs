using BasinTakip.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Entities.Base
{
   public class Event :EntityBase<int>
    {
        [Required(ErrorMessage = "* Zorunlu Alan")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "En az 3 karakter girmelisiniz")]
        [Display(Name = "Etkinlik Ad")]
        public string Name { get; set; }

        [Required(ErrorMessage = "* Zorunlu Alan")]
        [DataType(DataType.DateTime)]
        [Display(Name ="Başlangıç Tarihi")]
        public DateTime BeginDate { get; set; }

        [Required(ErrorMessage = "* Zorunlu Alan")]
        [DataType(DataType.DateTime)]
        [Display(Name ="Bitiş Tarihi")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "* Zorunlu Alan")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "En az 3 karakter girmelisiniz")]
        [Display(Name = "Etkinlik Yeri")]
        public string EventPlace { get; set; }

        [Required(ErrorMessage = "* Zorunlu Alan")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "En az 3 karakter girmelisiniz")]
        [Display(Name = "Etkinlik Adresi")]
        public string EventAdress { get; set; }

        [Required(ErrorMessage = "* Zorunlu Alan")]
        [Display(Name="Etkinlik Türü")]
        public int EventTypeId { get; set; } //picklistId

        [Required(ErrorMessage = "* Zorunlu Alan")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "En az 3 karakter girmelisiniz")]
        [Display(Name = "Etkinlik Notu")]
        public string EventNotes { get; set; }

        public string Url { get; set; }


    }
}
