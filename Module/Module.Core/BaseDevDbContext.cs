using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Core
{
    public abstract class BaseDevDbContext<TContext> : BaseDbContext where TContext : DbContext
    {
        static BaseDevDbContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TContext>());

        }
    }
}
