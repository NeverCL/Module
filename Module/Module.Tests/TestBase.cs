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

        protected TestBase()
        {

        }

        protected TestBase(params Type[] types) : this(new ContainerBuilder(), null, types)
        {

        }

        protected TestBase(ContainerBuilder builder, params Type[] types) : this(builder, null, types)
        {

        }

        protected TestBase(int[] interceptTypes, params Type[] types) : this(new ContainerBuilder(), interceptTypes, types)
        {

        }

        /// <summary>
        /// 指定Type开启ValidateInterceptor
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="interceptTypes"></param>
        /// <param name="types"></param>
        public TestBase(ContainerBuilder builder, int[] interceptTypes, params Type[] types)
        {
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
            if (interceptTypes != null)
            {
                builder.RegisterType<ValidateInterceptor>();
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
