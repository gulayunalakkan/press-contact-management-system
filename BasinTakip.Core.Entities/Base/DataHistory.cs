using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Core.Entities.Base
{
    public class DataHistory
    {
        public int Id { get; set; }
        public string EntityType { get; set; }
        public string EntityId { get; set; }
        public string Data { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedMemberId { get; set; }
        public string UserAgent { get; set; }
        public string UserHostAddress { get; set; }
    }
}
