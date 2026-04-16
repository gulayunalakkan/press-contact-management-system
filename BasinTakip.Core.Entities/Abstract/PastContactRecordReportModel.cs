using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasinTakip.Web.Models
{
    public class PastContactRecordReportModel
    {
        public int Id { get; set; }
        public string firstNamelastName { get; set; }
        public string Note { get; set; }
        public string Note2 { get; set; }
        public string lcvName { get; set; }
        public string particialName { get; set; }
        public DateTime date { get; set; }
        public string ContactBackName { get; set; }
        public int ContactId { get; set; }
        public string EditionNames { get; set; }
        public string Email2 { get; set; }
        public string districtName { get; set; }
        public string About { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Adress { get; set; }

        //temas edilmeyen kişiler raporu için
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string TaskName { get; set; }
        public string EditionName { get; set; }
        public string CountryName { get; set; }
        public DateTime? LastContactDate { get; set; }

        //Etkinlik raporu ve gerçekleşen son temas raporu için
        public string EventName { get; set; }
        public string EventPlacename { get; set; }
        public string EventTypeName { get; set; }
        public DateTime EventDate { get; set; }

        public List<PastContactRecordReportModel> NoContactPressMemberList { get; set; }
        public List<PastContactRecordReportModel> UpComingEVentList { get; set; }
        public List<PastContactRecordReportModel> HappeningLastContactList { get; set; }
        public List<PastContactRecordReportModel> MountBirthDate { get; set; }

        public int ContactKindId { get; set; }
        public int ContactTypeId { get; set; }
        public int? ContactSubId { get; set; }
        public int EditionId { get; set; }

        public string ContactTypeName { get; set; }
        public string ContactSubName { get; set; }

        public DateTime? PressMemberBirthDate { get; set; }
    }
}