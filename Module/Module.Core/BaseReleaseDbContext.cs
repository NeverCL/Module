using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Core
{
    /// <summary>
    /// 生产发布继承数据库
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TMigrationsConfiguration"></typeparam>
    public class BaseReleaseDbContext<TContext, TMigrationsConfiguration> : BaseDbContext
        where TContext : DbContext, new()
        where TMigrationsConfiguration : DbMigrationsConfiguration<TContext>, new()
    {
        static BaseReleaseDbContext()
        {
            //使用Migrations
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TContext, TMigrationsConfiguration>());
            //加速
            using (var db = new TContext())
            {
                var objectContext = ((IObjectContextAdapter)db).ObjectContext;
                var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
                mappingCollection.GenerateViews(new List<EdmSchemaError>());
            }
        }
    }
}
