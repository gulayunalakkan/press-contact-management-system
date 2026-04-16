using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Repository;
using BasinTakip.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;

namespace BasinTakip.EntityFramework.Repository
{
   public class VehicleRepository : GenericRepository<CommonContext, Vehicle, int>, IVehicleRepository
    {
        public VehicleRepository(CommonContext context)
            : base(context)
        {
        }

        protected override Expression<Func<Vehicle, bool>> FindyByKeyExpression(int key)
        {
            return p => p.Id == key;
        }
        //public override Vehicle Save(Vehicle entity)
        //{
        //    bool entityExistsInDb = DbSet.Any(FindyByKeyExpression(entity.Id));

        //    if (entityExistsInDb)
        //    {
        //        if (!DbSet.Local.Any(p => p == entity))
        //        {
        //            DbSet.Attach(entity);
        //        }

        //        Context.Entry(entity).State = EntityState.Modified;
        //    }
        //    else
        //    {
        //        DbSet.Add(entity);
        //    }

        //    Context.SaveChanges();

        //    return entity;
        //}
    }
}
