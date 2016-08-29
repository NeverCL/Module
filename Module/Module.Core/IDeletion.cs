using System;

namespace Module.Core
{
    public interface IDeletion : ISoftDelete
    {
        DateTime? DeletionTime { get; set; }
        string DeleterUserId { get; set; }
    }
}
