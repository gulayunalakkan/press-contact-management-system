using BasinTakip.Domain.Entities.Abstract;
using BasinTakip.Domain.Manager;
using BasinTakip.Domain.Repository;
using BasinTakip.EntityFramework.Repository;
using BasinTakip.Web.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IocManager.Install();

            using (IocManager.BeginScope())
            {
                var manager = IocManager.Resolve<IFirmManager>();

                var firm = manager.Filter(p => p.Id == 1).FirstOrDefault();

                manager.Save(firm);
            }

            IocManager.Dispose();
        }
    }
}
