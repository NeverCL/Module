﻿using System;
using System.Collections.Generic;
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
        static BaseDevDbContext()
        {
            //模型改变 即重建数据库
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TContext>());
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
