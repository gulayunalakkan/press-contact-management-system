using BasinTakip.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Entities.Base
{
    /// <summary>
    /// Temas Kaydı
    /// </summary>
    public class ContactRecord : EntityBase<int>
    {
        [Display(Name = "Notlar")]
        public string Notes { get; set; }

        [Required(ErrorMessage = "* Zorunlu Alan")]
        public int PressMemberId { get; set; }   //bağlı olduğu yayın

        [DisplayName("Temas Tarihi")]
        [DataType(DataType.Date)]
        public DateTime ContactDate { get; set; }  //Temas başlangıç tarihi 
        public DateTime? ContactEndDate { get; set; } // Temas bitiş tarihi

        [Required(ErrorMessage = "* Zorunlu Alan")]
        public int ContactTypeId { get; set; }  //PickList Temas şekli üst kategori

        public int? LcvId { get; set; }            //picklİST  (katılıyor,katılmıyor,belki)
        public int? participationStatus { get; set; }   //picklist katılım durumu (katıldı ,katılmadı)
        public int? accidentStatus { get; set; }      //kaza durumu

        [Required(ErrorMessage = "* Zorunlu Alan")]
        public int EditionId { get; set; }

        public int? ContactTypeSubId { get; set; }  //picklist alt kategori

        [Required(ErrorMessage = "* Zorunlu Alan")]
        public int ContactKindId { get; set; }     // picklist temas türü  (Etkinlik,Araç Tahsisi)
        
        

    } 
}
