using BasinTakip.Core.Business;
using BasinTakip.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Manager
{
   public interface IExportManager:IManager
    {
        void Save(string filePath);
        Stream TableReport(ReportInput input);
  }
}
