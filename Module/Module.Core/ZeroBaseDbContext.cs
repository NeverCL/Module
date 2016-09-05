using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Module.Core
{
    public class ZeroBaseDbContext<TUser, TRole> : IdentityDbContext<TUser, TRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
        where TUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
        where TRole : IdentityRole<string, IdentityUserRole>
    {

    }
}
