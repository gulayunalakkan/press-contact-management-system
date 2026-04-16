using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Core.Entities.Base
{
    public class SearchTable
    {
        public int Id { get; set; }
        public string EntityId { get; set; }
        public string EntityType { get; set; }
        public string SearchData { get; set; }
    }
}
