using BasinTakip.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Core.Entities.Abstract
{
    public class GenericListInput<TEntity, TKey> : ListInput
        where TEntity : IEntity<TKey>
    {
    }
}
