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
            string tokenValue = Guid.NewGuid().ToString("n");
            context.Ticket.Properties.IssuedUtc = DateTime.UtcNow;
            context.Ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddDays(60);
            RefreshTokens[tokenValue] = context.SerializeTicket();//加密当前ticket
            context.SetToken(tokenValue);
            await base.CreateAsync(context);
        }

        /// <summary>
        /// 在grant_type=refresh_token 且 经过ValidateClientAuthentication时调用
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            string value;
            if (RefreshTokens.TryRemove(context.Token, out value))
            {
                context.DeserializeTicket(value);
            }
            return base.ReceiveAsync(context);
        }
    }
}
