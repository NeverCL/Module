using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Core
{
    public interface ICreatorEntity
    {
        string CreatorId { get; set; }
        DateTime CreateTime { get; set; }
    }
}
