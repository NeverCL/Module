using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Infrastructure;

namespace Module.OAuth
{
    public class RefreshProvider : AuthenticationTokenProvider
    {
        private static readonly ConcurrentDictionary<string, string> RefreshTokens = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// 在每次生成AccessToken的时候都会调用
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            context.Ticket.Properties.IssuedUtc = DateTime.UtcNow;
            context.Ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddDays(60);
            var token = context.SerializeTicket();//加密当前ticket
            context.SetToken(SaveToken(token));
            await base.CreateAsync(context);
        }

        /// <summary>
        /// 保存refreshToken并返回content token
        /// </summary>
        /// <param name="refreshToken">真实token</param>
        /// <returns>response 中的token</returns>
        protected virtual string SaveToken(string refreshToken)
        {
            var key = Guid.NewGuid().ToString("n");
            RefreshTokens[key] = refreshToken;
            return key;
        }

        /// <summary>
        /// 移除content token并返回真实refreshToken
        /// </summary>
        /// <param name="key">content token</param>
        /// <returns>refreshToken</returns>
        protected virtual string RemoveToken(string key)
        {
            string token;
            RefreshTokens.TryRemove(key, out token);
            return token;
        }

        /// <summary>
        /// 在grant_type=refresh_token 且 经过ValidateClientAuthentication时调用
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var token = RemoveToken(context.Token);
            if (!string.IsNullOrEmpty(token))
                context.DeserializeTicket(token);
            return base.ReceiveAsync(context);
        }
    }
}
