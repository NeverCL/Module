using System.Data.Entity;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Module.Application
{
    public abstract class BaseApplicationService
    {
        protected string GetUserId()
        {
            var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (claimsPrincipal == null)
                return null;
            var claimsIdentity = claimsPrincipal.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
                return null;
            return claimsIdentity.GetUserId();
        }
    }

    public abstract class BaseApplicationService<T> where T : DbContext
    {
        protected T CurrentDb { set; get; }

        protected async Task<int> SaveChangesAsync()
        {
            return await CurrentDb.SaveChangesAsync();
        }

        protected int SaveChanges()
        {
            return CurrentDb.SaveChanges();
        }

        protected string GetUserId()
        {
            var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (claimsPrincipal == null)
                return null;
            var claimsIdentity = claimsPrincipal.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
                return null;
            return claimsIdentity.GetUserId();
        }
    }
}
