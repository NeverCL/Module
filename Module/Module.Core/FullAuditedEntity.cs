using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Core
{
    public class FullAuditedEntity<T> : AuditedEntity<T>, IDeletionAudited
    {
        public string DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class FullAuditedEntity : FullAuditedEntity<long>
    {

    }
}
