using BasinTakip.Core.Business;
using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Manager
{
   public interface IPickListManager : IGenericManager<IPickListRepository, PickList, int>
    {
    }
}
