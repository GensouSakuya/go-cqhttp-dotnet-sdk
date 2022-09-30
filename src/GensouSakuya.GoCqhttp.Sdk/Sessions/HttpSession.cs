using GensouSakuya.GoCqhttp.Sdk.Models.Guild;
using GensouSakuya.GoCqhttp.Sdk.Models.Responses;
using RestSharp;
using System;
using System.Collections.Generic;
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

        public override Task<string> CreateGuildRole(string guildId, string color, string name, bool independent, List<string> initialUsers)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteGuildRole(string guildId, string roleId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (_httpClient != null)
            {
                _httpClient.Dispose();
            }
        }

        public override Task<List<ChannelInfo>?> GetGuildChannelList(string guildId, bool noCache)
        {
            throw new NotImplementedException();
        }

        public override Task<List<GuildInfo>?> GetGuildList()
        {
            throw new NotImplementedException();
        }

        public override Task<GetGuildMemberListResponse> GetGuildMemberList(string guildId, string nextToken)
        {
            throw new NotImplementedException();
        }

        public override async Task<GuildMemberInfo?> GetGuildMemberProfile(string guildId, string tinyId)
        {
            var request = new RestRequest("/get_guild_member_profile");
            request.Method = Method.Get;
            request.AddParameter("guild_id", guildId);
            request.AddParameter("user_id", tinyId);
            var response = await _httpClient.ExecuteAsync<GuildMemberInfo>(request);
            response.ThrowIfError();
            return response.Data;
        }

        public override Task<GuildGuestMeta?> GetGuildMetaByGuest(string guildId)
        {
            throw new NotImplementedException();
        }

        public override Task<List<RoleInfo>> GetGuildRoles(string guildId)
        {
            throw new NotImplementedException();
        }

        public override Task<GuildProfile?> GetGuildServiceProfile()
        {
            throw new NotImplementedException();
        }

        public override Task<List<FeedInfo>?> GetTopicChannelFeeds(string guildId, string channelId)
        {
            throw new NotImplementedException();
        }

        public override Task<string?> SendGuildChannelMsg(string guildId, string channelId, string msg)
        {
            throw new NotImplementedException();
        }

        public override Task SetGuildMemberRole(string guildId, bool set, string roleId, List<string> users)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateGuildRole(string guildId, string roleId, string name, string color, bool independent)
        {
            throw new NotImplementedException();
        }
    }
}
