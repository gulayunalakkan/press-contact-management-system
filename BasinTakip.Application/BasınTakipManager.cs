using BasinTakip.Application.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Application
{
   public static class BasınTakipManager
    {
        private static ReportOperation p_Report = null;
        public static ReportOperation Report
        {
            get
            {
                if (p_Report == null)
                {
                    p_Report = new ReportOperation();
                }
                return p_Report;
            }
        }
        private static ExportManager p_Export = null;
        public static ExportManager Export
        {
            get
            {
                if (p_Export == null)
                {
                    p_Export = new ExportManager();
                }
                return p_Export;
            }
        }
    }
}
