using Xunit;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Module.Core.Tests
{
    public class BaseDbContextTests
    {
        //static BaseDbContextTests()
        //{
        //    Database.DefaultConnectionFactory = new OfflineProviderFactory();

        //}
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
            using (var db = new AppDb(connection))
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
        public void TestZeroDb()
        {
            DbConnection connection = Effort.DbConnectionFactory.CreateTransient();
            using (var db = new ZeroDb(connection))
            {
                db.Users.Add(new ZeroUser {DisplayName = "系统管理员", UserName = "admin"});
                db.SaveChanges();
                var user = db.Users.Single(x => x.UserName == "admin");
                Assert.NotNull(user);

                db.Users.Remove(user);
                db.SaveChanges();
                user = db.Users.Single(x => x.UserName == "admin");
                Assert.NotNull(user);
                Assert.True(user.DeletionTime.Value.Date == DateTime.Now.Date);
            }
        }
    }

    public class ZeroDb : ZeroBaseDbContext
    {
        public ZeroDb(DbConnection connection) : base(connection)
        {
        }
    }


    public class AppDb : BaseDbContext
    {
        public AppDb(DbConnection connection) : base(connection)
        {
        }

        public DbSet<User> Users { get; set; }
    }

    public class User : FullAuditedEntity
    {
        public string Name { get; set; }
    }
}
