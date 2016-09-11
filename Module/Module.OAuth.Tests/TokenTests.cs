using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Should;
using Xunit;

namespace Module.OAuth.Tests
{
    public class TokenTests
    {
        [Fact]
        public async Task TestGrant()
        {
            var url = "http://localhost:12345/";
            using (WebApp.Start<Startup>(url))
            {
                var client = new HttpClient();
                var rst = await client.PostAsync(url + "token", new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"client_id", "1234"},
                    {"client_secret", "1234"},
                    {"grant_type", "client_credentials"},//client_credentials/password/refresh_token
                    {"username","123"},
                    {"password","1234"}
                }));
                var response = await rst.Content.ReadAsStringAsync();
                var accessToken = JObject.Parse(response)["access_token"].Value<string>();
                accessToken.ShouldNotBeNull();
                var refreshToken = JObject.Parse(response)["refresh_token"].Value<string>();
                refreshToken.ShouldNotBeNull();

                rst = await client.PostAsync(url + "token", new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"client_id", "1234"},
                    {"client_secret", "1234"},
                    {"grant_type", "password"},//client_credentials/password/refresh_token
                    {"username","1234"},
                    {"password","1234"}
                }));
                response = await rst.Content.ReadAsStringAsync();
                accessToken = JObject.Parse(response)["access_token"].Value<string>();
                accessToken.ShouldNotBeNull();
                refreshToken = JObject.Parse(response)["refresh_token"].Value<string>();
                refreshToken.ShouldNotBeNull();


                //refresh_token
                rst = await client.PostAsync(url + "token", new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"grant_type", "refresh_token"},//client_credentials/password/refresh_token
                    {"refresh_token",refreshToken}
                }));
                response = await rst.Content.ReadAsStringAsync();
                var newAccessToken = JObject.Parse(response)["access_token"].Value<string>();
                newAccessToken.ShouldNotBeNull();
                newAccessToken.ShouldNotEqual(accessToken);
                var newRefreshToken = JObject.Parse(response)["refresh_token"].Value<string>();
                newRefreshToken.ShouldNotBeNull();
                newRefreshToken.ShouldNotEqual(refreshToken);
            }
        }



    }

}
