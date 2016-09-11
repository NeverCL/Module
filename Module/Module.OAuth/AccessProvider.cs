using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace Module.OAuth
{
    /// <summary>
    /// AccessToken 提供者
    /// </summary>
    public class AccessProvider : OAuthAuthorizationServerProvider
    {
        //先验证客户端是否通过 
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId, clientSecret;
            //forms 和 basic
            if (!context.TryGetFormCredentials(out clientId, out clientSecret))
            {
                context.TryGetBasicCredentials(out clientId, out clientSecret);
            }
            //validate
            if (ValidateClient(clientId, clientSecret))
            {
                context.Validated(clientId);//调用该方法表示验证通过
            }
            return base.ValidateClientAuthentication(context);
        }

        //再分配客户端令牌
        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var oAuthIdentity = CreateClientClaims(context);
            //var ticket = new AuthenticationTicket(oAuthIdentity, new AuthenticationProperties());
            context.Validated(oAuthIdentity);//调用该方法表示分配令牌
            //context.OwinContext.Response.Headers["Access-Control-Allow-Origin"] = "http://localhost:9527";//支持cros
            return base.GrantClientCredentials(context);
        }

        //使用User 身份验证
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //validate
            if (ValidateUser(context.UserName, context.Password))
            {
                var oAuthIdentity = CreateUserClaims(context);
                context.Validated(oAuthIdentity);//调用该方法表示分配令牌
            }
            return base.GrantResourceOwnerCredentials(context);
        }

        /// <summary>
        /// 验证Client
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <returns></returns>
        protected virtual bool ValidateClient(string clientId, string clientSecret)
        {
            return clientId == clientSecret;
        }

        /// <summary>
        /// 验证用户密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        protected virtual bool ValidateUser(string userName, string password)
        {
            return userName == password;
        }

        /// <summary>
        /// 创建Client 声明信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual ClaimsIdentity CreateClientClaims(OAuthGrantClientCredentialsContext context)
        {
            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, context.ClientId));
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Role, "App"));
            return oAuthIdentity;
        }

        /// <summary>
        /// 创建用户 声明信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual ClaimsIdentity CreateUserClaims(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            oAuthIdentity.AddClaim(new Claim(ClaimTypes.Role, "User"));
            return oAuthIdentity;
        }
    }
}
