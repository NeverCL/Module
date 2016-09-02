using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Core
{
    public class CreationAuditedEntity<T> : BaseEntity<T>, ICreatorEntity
    {
        public string CreatorId { get; set; }
        public DateTime CreateTime { get; set; }
    }
    public class CreationAuditedEntity : CreationAuditedEntity<long>
    {

    }
}
