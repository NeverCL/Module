using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Core
{
    public class CreatorEntity<T> : BaseEntity<T>, ICreatorEntity
    {
        public string CreatorId { get; set; }
        public DateTime CreateTime { get; set; }
    }
    public class CreatorEntity : CreatorEntity<long>
    {

    }
}
