using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Core
{
    public abstract class BaseDevDbContext<TContext> : BaseDbContext
        where TContext : DbContext, new()
    {
        #region ctor
        protected BaseDevDbContext()
: this("DefaultConnection")
        {

        }

        protected BaseDevDbContext(DbConnection connection) : base(connection)
        {

        }

        protected BaseDevDbContext(string connStr)
            : base(connStr)
        {

        }
        #endregion

        static BaseDevDbContext()
        {
            //模型改变 即重建数据库
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TContext>());
            GenerateViews(new TContext());
        }
    }
}
