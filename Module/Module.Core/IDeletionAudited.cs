using System;

namespace Module.Core
{
    public interface IDeletionAudited : ISoftDelete
    {
        DateTime? DeletionTime { get; set; }
        string DeleterUserId { get; set; }
    }
}
