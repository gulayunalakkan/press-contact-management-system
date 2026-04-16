using BasinTakip.Core.Entities.Abstract;
using BasinTakip.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace BasinTakip.Web.Models
{
    public class PickListListModel: GenericListInput<PickList, int>
    {
        public int? CategoryId { get; set; }
    }
}