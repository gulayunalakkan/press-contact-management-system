using BasinTakip.Core.Web;
using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Manager;
using BasinTakip.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasinTakip.Core.Entities.Abstract;

namespace BasinTakip.Web.Controllers
{
    public class PickListCategoryController : BaseController<IPickListCategoryManager, IPickListCategoryRepository, PickListCategory, int>
    {
        public PickListCategoryController(IPickListCategoryManager manager) 
            : base(manager)
        {
        }
    }
}