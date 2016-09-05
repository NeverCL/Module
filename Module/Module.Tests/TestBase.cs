using System;
using System.Data.Common;
using Autofac;

namespace Module.Tests
{
    public class TestBase
    {
        protected IContainer Container { get; set; }

        public TestBase(params Type[] types)
        {
            var builder = new ContainerBuilder();
            foreach (var type in types)
            {
                builder.RegisterAssemblyTypes(type.Assembly).AsImplementedInterfaces().AsSelf();
            }
            Container = builder.Build();
        }

        protected T GetService<T>()
        {
            return Container.Resolve<T>();
        }

        protected DbConnection CreateDbConnection()
        {
            return Effort.DbConnectionFactory.CreateTransient();
        }
    }
}
