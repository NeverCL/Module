using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Module.Core
{
    /// <summary>
    /// Zero默认角色
    /// </summary>
    public class ZeroRole : IdentityRole, IDeletionAudited
    {
        public ZeroRole()
        {

        }

        public ZeroRole(string roleName) : this(roleName, Guid.NewGuid().ToString())
        {
        }

        public ZeroRole(string roleName, string id) : base(roleName)
        {
            this.Id = id;
            this.DisplayName = roleName;
        }

        /// <summary>
        /// 昵称
        /// </summary>
        public string DisplayName { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string DeleterUserId { get; set; }
    }
}
