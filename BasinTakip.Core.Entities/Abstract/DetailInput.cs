using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Core.Entities.Abstract
{
    public class DetailInput<TKey>
    {
        public TKey Id { get; set; }
    }
}
