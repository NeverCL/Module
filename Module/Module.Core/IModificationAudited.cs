using System;

namespace Module.Core
{
    public interface IModificationAudited
    {
        DateTime? LastModificationTime { get; set; }
        string LastModifierUserId { get; set; }
    }
}
