using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Module.Core
{
    /// <summary>
    /// Zero默认用户
    /// </summary>
    public class ZeroUser : IdentityUser, IDeletionAudited
    {
        public ZeroUser()
        {

        }

        public ZeroUser(string userName) : this(userName, Guid.NewGuid().ToString())
        {
        }

        public ZeroUser(string userName, string id) : base(userName)
        {
            this.Id = id;
            this.DisplayName = userName;
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
