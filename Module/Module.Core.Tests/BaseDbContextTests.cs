using Xunit;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System;

namespace Module.Core.Tests
{
    public class BaseDbContextTests
    {
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
