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
    public class ZeroRole : IdentityRole
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string DisplayName { get; set; }
    }
}
