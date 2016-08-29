using System;

namespace Module.Core
{
    public interface IModification
    {
        DateTime? LastModificationTime { get; set; }
        string LastModifierUserId { get; set; }
    }
}
