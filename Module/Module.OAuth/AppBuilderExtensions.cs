using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Module.OAuth;

namespace Owin
{
    public static class AppBuilderExtensions
    {
        public static void UseModuleOAuth(this IAppBuilder app, AccessProvider accessProvider, RefreshProvider refreshProvider)
        {
            app.UseOAuthBearerTokens(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = accessProvider,
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),//默认20分钟
                AllowInsecureHttp = true,
                RefreshTokenProvider = refreshProvider
            });
        }

        public static void UseModuleOAuth(this IAppBuilder app, AccessProvider accessProvider)
        {
            UseModuleOAuth(app, accessProvider, new RefreshProvider());
        }

        public static void UseModuleOAuth(this IAppBuilder app, RefreshProvider refreshProvider)
        {
            UseModuleOAuth(app, new AccessProvider(), refreshProvider);
        }

        public static void UseModuleOAuth(this IAppBuilder app)
        {
            UseModuleOAuth(app, new AccessProvider(), new RefreshProvider());
        }
    }
}
