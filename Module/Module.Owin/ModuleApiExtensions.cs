using System;
using System.Linq;
using System.Web.Http;
using Autofac;
using Autofac.Extras.DynamicProxy2;
using Autofac.Integration.WebApi;
using Module.Application.Validate;
using Owin;

namespace Module.Owin
{
    public static class ModuleApiExtensions
    {
        public static IAppBuilder UseModuleApi(this IAppBuilder app, ContainerBuilder builder, int[] interceptTypes, params Type[] types)
        {
            ConfigApi(app, "api/{controller}/{action}/{id}", builder, interceptTypes, types);
            return app;
        }

        public static IAppBuilder UseModuleApi(this IAppBuilder app, ContainerBuilder builder, params Type[] types)
        {
            ConfigApi(app, "api/{controller}/{action}/{id}", builder, null, types);
            return app;
        }

        public static IAppBuilder UseModuleApi(this IAppBuilder app, params Type[] types)
        {
            ConfigApi(app, "api/{controller}/{action}/{id}", new ContainerBuilder(), null, types);
            return app;
        }

        public static IAppBuilder UseModuleApi(this IAppBuilder app, int[] interceptTypes, params Type[] types)
        {
            ConfigApi(app, "api/{controller}/{action}/{id}", new ContainerBuilder(), interceptTypes, types);
            return app;
        }

        private static void ConfigApi(IAppBuilder app, string routeTemplate, ContainerBuilder builder, int[] interceptTypes, params Type[] types)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: routeTemplate,
               defaults: new { id = RouteParameter.Optional }
            );
            if (interceptTypes != null && interceptTypes.Any())
            {
                builder.RegisterType<ValidateInterceptor>();
            }
            for (var i = 0; i < types.Length; i++)
            {
                var type = types[i];
                if (interceptTypes != null && interceptTypes.Contains(i))
                {
                    //注意:此处不能调用AsSelf() 否则无法resolve
                    builder.RegisterAssemblyTypes(type.Assembly).AsImplementedInterfaces().EnableInterfaceInterceptors().InterceptedBy(typeof(ValidateInterceptor));
                }
                else
                {
                    builder.RegisterAssemblyTypes(type.Assembly).AsImplementedInterfaces().AsSelf();
                }
            }
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            app.UseWebApi(config);
        }
    }
}
