using System;
using System.Data.Common;
using System.Linq;
using Autofac;
using Autofac.Extras.DynamicProxy2;
using Module.Application.Validate;

namespace Module.Tests
{
    public class TestBase
    {
        protected IContainer Container { get; set; }

        public TestBase(params Type[] types) : this(null, types)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interceptTypes">指定Type开启ValidateInterceptor</param>
        /// <param name="types"></param>
        public TestBase(int[] interceptTypes, params Type[] types)
        {
            var builder = new ContainerBuilder();
            for (var i = 0; i < types.Length; i++)
            {
                var type = types[i];
                if (interceptTypes != null && interceptTypes.Contains(i))
                {
                    builder.RegisterAssemblyTypes(type.Assembly).AsImplementedInterfaces().AsSelf().EnableInterfaceInterceptors().InterceptedBy(typeof(ValidateInterceptor));
                }
                else
                {
                    builder.RegisterAssemblyTypes(type.Assembly).AsImplementedInterfaces().AsSelf();
                }
            }
            Container = builder.Build();
        }

        protected virtual T GetService<T>()
        {
            return Container.Resolve<T>();
        }

        protected DbConnection CreateDbConnection()
        {
            return Effort.DbConnectionFactory.CreateTransient();
        }
    }
}
