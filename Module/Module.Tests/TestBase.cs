using System.Data.Common;
using Autofac;

namespace Module.Tests
{
    public class TestBase
    {
        protected IContainer Container { get; set; }

        protected T GetService<T>()
        {
            //TODO Container DI?
            return Container.Resolve<T>();
        }

        protected DbConnection CreateDbConnection()
        {
            return Effort.DbConnectionFactory.CreateTransient();
        }
    }
}
