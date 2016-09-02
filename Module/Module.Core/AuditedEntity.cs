using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Core
{
    /// <summary>
    /// 记录添加和修改记录
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AuditedEntity<T> : CreationAuditedEntity<T>, IModificationAudited
    {
        public DateTime? LastModificationTime { get; set; }
        public string LastModifierUserId { get; set; }
    }

    public class AuditedEntity : AuditedEntity<long>
    {

    }
}
