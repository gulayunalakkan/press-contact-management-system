using BasinTakip.Domain.Entities.Base;
using BasinTakip.Domain.Repository;
using BasinTakip.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BasinTakip.EntityFramework.Repository
{
    public class FirmRepository : GenericRepository<CommonContext, Firm, int>, IFirmRepository
    {
        public FirmRepository(CommonContext context)
            : base(context)
        {
        }

        protected override Expression<Func<Firm, bool>> FindyByKeyExpression(int key)
        {
            return p => p.Id == key;
        }
    }
}
