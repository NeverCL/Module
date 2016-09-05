using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Common;

namespace Module.Core
{
    public class ZeroBaseDbContext<TUser, TRole> : IdentityDbContext<TUser, TRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
        where TUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
        where TRole : IdentityRole<string, IdentityUserRole>
    {
        #region ctor
        protected ZeroBaseDbContext()
    : this("DefaultConnection")
        {

        }

        protected ZeroBaseDbContext(DbConnection connection) : base(connection, true)
        {
        }

        protected ZeroBaseDbContext(string connStr)
            : base(connStr)
        {
        }
        #endregion

        #region GenerateViews
        protected static void GenerateViews(DbContext db)
        {
            using (db)
            {
                var objectContext = ((IObjectContextAdapter)db).ObjectContext;
                var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
                mappingCollection.GenerateViews(new List<EdmSchemaError>());
            }
        }
        #endregion

        #region SaveChanges
        public override int SaveChanges()
        {
            try
            {
                ApplyConcepts();
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                LogDbEntityValidationException(ex);
                throw;
            }
        }
        public override Task<int> SaveChangesAsync()
        {
            try
            {
                ApplyConcepts();
                return base.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                LogDbEntityValidationException(ex);
                throw;
            }
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                ApplyConcepts();
                return base.SaveChangesAsync(cancellationToken);
            }
            catch (DbEntityValidationException ex)
            {
                LogDbEntityValidationException(ex);
                throw;
            }
        }
        private void LogDbEntityValidationException(DbEntityValidationException dbEntityValidationException)
        {
        }
        #endregion

        #region ApplyConcepts
        private void ApplyConcepts()
        {
            foreach (DbEntityEntry entry in this.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        SetCreationAuditProperties(entry);
                        continue;
                    case EntityState.Deleted:
                        HandleSoftDelete(entry);
                        continue;
                    case EntityState.Modified:
                        SetModificationAuditProperties(entry);
                        if (entry.Entity is ISoftDelete && (entry.Entity as ISoftDelete).IsDeleted)
                        {
                            HandleSoftDelete(entry);
                        }
                        continue;
                    default:
                        continue;
                }
            }
        }
        private void SetModificationAuditProperties(DbEntityEntry entry)
        {
            if (entry.Entity is IModificationAudited)
            {
                var entity = entry.Entity as IModificationAudited;
                entity.LastModificationTime = DateTime.Now;
                entity.LastModifierUserId = GetUserId();
            }
        }
        private void HandleSoftDelete(DbEntityEntry entry)
        {
            if (entry.Entity is ISoftDelete)
            {
                entry.State = EntityState.Unchanged;
                var entity = entry.Entity as ISoftDelete;
                entity.IsDeleted = true;
                if (entry.Entity is IDeletionAudited)
                {
                    var deletionEntity = entry.Entity as IDeletionAudited;
                    deletionEntity.DeletionTime = DateTime.Now;
                    deletionEntity.DeleterUserId = GetUserId();
                }
            }
        }
        private void SetCreationAuditProperties(DbEntityEntry entry)
        {
            if (entry.Entity is ICreatorEntity)
            {
                var entity = entry.Entity as ICreatorEntity;
                entity.CreateTime = DateTime.Now;
                entity.CreatorId = GetUserId();
            }
        }
        private string GetUserId()
        {
            var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            if (claimsPrincipal == null)
                return null;
            var claimsIdentity = claimsPrincipal.Identity as ClaimsIdentity;
            if (claimsIdentity == null)
                return null;
            return claimsIdentity.GetUserId();
        }
        #endregion

        #region OnModelCreating
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }

    public class ZeroBaseDbContext : ZeroBaseDbContext<ZeroUser, ZeroRole>
    {
        #region ctor
        protected ZeroBaseDbContext()
    : this("DefaultConnection")
        {

        }

        protected ZeroBaseDbContext(DbConnection connection) : base(connection)
        {
        }

        protected ZeroBaseDbContext(string connStr)
            : base(connStr)
        {
        }
        #endregion
    }
}
