using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Core
{
    public class BaseEntity : BaseEntity<long>
    {
    }

    public class BaseEntity<T> : IEntity<T>
    {
        public T Id { get; set; }
    }
}
