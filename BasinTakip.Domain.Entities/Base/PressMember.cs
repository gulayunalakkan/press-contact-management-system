using BasinTakip.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Entities.Base
{
    /// <summary>
    /// Basın mensubu
    /// </summary>
    public class PressMember : EntityBase<int>
    {
        [Required(ErrorMessage = "* Zorunlu Alan")]
        [Display(Name = "Ad")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "En az 3 karakter girmelisiniz")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "* Zorunlu Alan")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "En az 3 karakter girmelisiniz")]
        [Display(Name = "Soyad")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "* Zorunlu Alan")]
        [Display(Name = "Cep Telefonu ( 555-555-5555 )")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Geçerli numara giriniz")]
        public string MobilePhone { get; set; }

        [Display(Name = "Faks")]
        public string Fax { get; set; }

        [Required(ErrorMessage = "* Zorunlu Alan")]
        [Display(Name = "E-Mail")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",ErrorMessage = "Geçerli bir Email giriniz")]
        public string Email { get; set; }


        [Display(Name = "E-Mail2")]
        [DataType(DataType.EmailAddress)]
        public string Email2 { get; set; }
        
        
        [Display(Name = "Doğum Tarihi")]
        [DataType(DataType.DateTime)]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "* Zorunlu Alan")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "En az 3 karakter girmelisiniz")]
        [Display(Name = "Adres")]
        public string Adress { get; set; }

        [Display(Name = "Notlar")]
        public string Notes { get; set; }

        [Display(Name = "Hakkında")]
        public string About { get; set; }

        public bool? Block { get; set; }

        [Required(ErrorMessage = "* Zorunlu Alan")]
        [Display(Name = "Yayınlar")]
        public int? EditionId { get; set; }

        [Required(ErrorMessage = "* Zorunlu Alan")]
        [Display(Name = "Yayınlar")]
        public string EditionIds { get; set; }
        public string EditionNames { get; set; }

        [Required(ErrorMessage = "* Zorunlu Alan")]
        [Display(Name = "Görev Bilgileri")]
        public int? TaskId { get; set; }   //PickList

        [Display(Name = "İl")]
        [Required(ErrorMessage = "* Zorunlu Alan")]
        public int CountryId { get; set; }

        [Display(Name = "İlçe")]
        [Required(ErrorMessage = "* Zorunlu Alan")]
        public int DistrictId { get; set; }   //ilçeId
        public string Avatar { get; set; }
        public string Path1 { get; set; }
        public string Path2 { get; set; }
        public string Path3 { get; set; }
        public string Path4 { get; set; }
        
    }
}
