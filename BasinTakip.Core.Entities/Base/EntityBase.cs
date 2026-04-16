using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Entities.Base
{
    public class EntityBase<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
        public string Permalink { get; set; }
        public int Order { get; set; }
        [Display(Name = "Aktif mi?")]
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedMemberId { get; set; }
        public string ModifiedMemberId { get; set; }
        public string DeletedMemberId { get; set; }
    }
}
