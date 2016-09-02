using Xunit;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System;
using System.Data.Entity.Infrastructure;

namespace Module.Core.Tests
{
    public class BaseDbContextTests
    {
        static BaseDbContextTests()
        {
            Database.DefaultConnectionFactory = new OfflineProviderFactory();

        }
        public class OfflineProviderFactory : IDbConnectionFactory
        {
            public DbConnection CreateConnection(string nameOrConnectionString)
            {
                return Effort.DbConnectionFactory.CreateTransient();
            }
        }

        [Fact]
        public void TestAudit()
        {
            DbConnection connection = Effort.DbConnectionFactory.CreateTransient();
            using (var db = new BaseAppDb(connection))
            {
                //create
                db.Users.Add(new User
                {
                    Name = "admin"
                });
                db.SaveChanges();
                var user = db.Users.Single(x => x.Id == 1);
                Assert.True(user != null);
                Assert.True(user.CreateTime.Date == DateTime.Now.Date);

                //modify
                user.Name = "hello";
                db.SaveChanges();
                user = db.Users.Single(x => x.Id == 1);
                Assert.True(user.LastModificationTime.Value.Date == DateTime.Now.Date);

                //delete
                db.Users.Remove(user);
                db.SaveChanges();
                user = db.Users.Single(x => x.Id == 1);
                Assert.True(user.IsDeleted == true);
                Assert.True(user.DeletionTime.Value.Date == DateTime.Now.Date);
            }
        }

        [Fact]
        public void TestDevDb()
        {
            using (var db = new BaseDevAppDb())
            {
                db.Users.Add(new User {Name = "admin"});
                db.SaveChanges();
                var user = db.Users.Single(x => x.Id == 1);
                Assert.True(user != null);
                Assert.True(user.CreateTime.Date == DateTime.Now.Date);
            }
        }
    }


    public class BaseDevAppDb : BaseDevDbContext<BaseDevAppDb>
    {
        public BaseDevAppDb()
        {

        }

        public BaseDevAppDb(DbConnection connection) : base(connection)
        {
        }

        public DbSet<User> Users { get; set; }
    }


    public class BaseAppDb : BaseDbContext
    {
        public BaseAppDb(DbConnection connection) : base(connection)
        {
        }

        public DbSet<User> Users { get; set; }
    }

    public class User : FullAuditedEntity
    {
        public string Name { get; set; }
    }
}
