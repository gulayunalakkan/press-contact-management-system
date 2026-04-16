using BasinTakip.Entities.Base;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BasinTakip.Core.Entities.Abstract
{
    public class GenericListOutput<TEntity, TKey> 
        where TEntity : IEntity<TKey>
    {
        public string SearchText { get; set; }
        public IPagedList<TEntity> PagedData { get; set; }
        public string[] filter { get; set; }
    }
}
