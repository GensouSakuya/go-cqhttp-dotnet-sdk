using GensouSakuya.GoCqhttp.Sdk.Sessions.Models.Responses.Guild;
using RestSharp;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GensouSakuya.GoCqhttp.Sdk.Sessions
{
    public class HttpSession : BaseSession, IDisposable
    {
        private Uri _httpUri;
        private RestClient _httpClient;

        public HttpSession(string host, int port, string accessToken, bool useSsl = false) : base(host, port, accessToken, useSsl)
        {
            var httpScheme = UseSsl ? "https" : "http";
            _httpUri = new Uri($"{httpScheme}://{Host}:{Port}");
            _httpClient = new RestClient(_httpUri);
        }

        public override Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            if (_httpClient != null)
            {
                _httpClient.Dispose();
            }
        }

        public override async Task<GuildMemberInfo?> GetGuildMemberProfile(long guildId, long tinyId)
        {
            var request = new RestRequest("/get_guild_member_profile");
            request.Method = Method.Get;
            request.AddParameter("guild_id", guildId);
            request.AddParameter("user_id", tinyId);
            var response = await _httpClient.ExecuteAsync<GuildMemberInfo>(request);
            response.ThrowIfError();
            return response.Data;
        }
    }
}
