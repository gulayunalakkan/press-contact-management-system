using BasinTakip.Core.Data;
using BasinTakip.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Core.Data
{
    public interface IDataHistoryRepository : IRepository
    {
        DataHistory Save(DataHistory entity);
    }
}
