using GensouSakuya.GoCqhttp.Sdk.Models.Guild;
using GensouSakuya.GoCqhttp.Sdk.Models.Responses;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using GensouSakuya.GoCqhttp.Sdk.Models.Requests;
using GensouSakuya.GoCqhttp.Sdk.Models.Group;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace GensouSakuya.GoCqhttp.Sdk.Sessions
{
    public class HttpSession : BaseSession, IDisposable
    {
        private Uri _httpUri;
        private RestClient _httpClient;

        public HttpSession(string host, int port, string accessToken, bool useSsl = false, ILogger? logger = null) : base(host, port, accessToken, useSsl, logger)
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

        public override Task<List<GroupMemberInfo>> GetGroupMemberList(string groupId, bool noCache = true)
        {
            return SendGet<List<GroupMemberInfo>>("/get_group_member_list", new GetGroupMemberListRequest(groupId,noCache));
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

        public override async Task<string> SendGroupMessage(string groupId, string message, bool autoEscape)
        {
            var res = await SendPost<SendMessageResponse>("/send_group_msg", new SendGroupMessageRequest(groupId, message, autoEscape));
            return res.MessageId;
        }

        public override async Task<string> SendGuildChannelMsg(string guildId, string channelId, string msg)
        {
            var res = await SendPost<SendMessageResponse>("/send_guild_channel_msg", new SendGuildChannelMsgRequest(guildId, channelId, msg));
            return res.MessageId;
        }

        public override async Task<string> SendPrivateMessage(string userId, string message, bool autoEscape = false, string? tempFromGroupId = null)
        {
            var res = await SendPost<SendMessageResponse>("/send_private_msg", new SendPrivateMessageRequest(userId, tempFromGroupId, message, autoEscape));
            return res.MessageId;
        }

        public override Task SetGuildMemberRole(string guildId, bool set, string roleId, List<string> users)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateGuildRole(string guildId, string roleId, string name, string color, bool independent)
        {
            throw new NotImplementedException();
        }

        private async Task<TRes> SendPost<TRes>(string path, object body) where TRes: class
        {
            var request = new RestRequest(path);
            request.Method = Method.Post;
            request.AddJsonBody(body);
            var response = await _httpClient.ExecuteAsync<TRes>(request);
            response.ThrowIfError();
            return response.Data;
        }

        private async Task<TRes> SendGet<TRes>(string path, object param) where TRes : class
        {
            var request = new RestRequest(path);
            request.Method = Method.Get;
            if(param != null)
            {
                var jobj = JObject.FromObject(param);
                foreach(var token in jobj.Children())
                {
                    var name = token.Path;
                    var value = token.Values().FirstOrDefault()?.ToString();
                    request.AddParameter(name, value);
                }
            }
            var response = await _httpClient.ExecuteAsync<TRes>(request);
            response.ThrowIfError();
            return response.Data;
        }
    }
}
