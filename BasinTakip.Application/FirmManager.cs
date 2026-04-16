using BasinTakip.Domain.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Repository;
using BasinTakip.Core;
using BasinTakip.Core.Business;

namespace BasinTakip.Application
{
    public class FirmManager : GenericManager<IFirmRepository, Firm, int>, IFirmManager
    {
    }
}
