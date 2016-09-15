## 说明

依赖

Install-Package Microsoft.Owin.Host.SystemWeb

Install-Package Microsoft.AspNet.WebApi.Owin

Install-Package Microsoft.Owin.Cors

Install-Package Microsoft.Owin.Security.Cookies

Install-Package Autofac


app.UseCors(CorsOptions.AllowAll);

ConfigLog

ConfigApi

UseCookieAuthentication


- UseModuleCookie("")

- UseModuleApi("")


