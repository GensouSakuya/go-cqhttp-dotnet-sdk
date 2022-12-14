using System;
using System.Threading;
using System.Threading.Tasks;
using GensouSakuya.GoCqhttp.Sdk.Sessions.Models.PostEvents.Base;
using Newtonsoft.Json;
using Websocket.Client;
using System.Collections.Concurrent;
using GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Base;
using GensouSakuya.GoCqhttp.Sdk.Models.Requests;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using GensouSakuya.GoCqhttp.Sdk.Models.Guild;
using GensouSakuya.GoCqhttp.Sdk.Models.Responses;
using Microsoft.Extensions.Logging;
using GensouSakuya.GoCqhttp.Sdk.Sessions.Drivers.Events;
using GensouSakuya.GoCqhttp.Sdk.Models.Group;

namespace GensouSakuya.GoCqhttp.Sdk.Sessions
{
    public class WebsocketSession : BaseSession, IAsyncDisposable
    {
        private Uri _wsUri;
        private WebsocketClient _wsClient;
        private ConcurrentDictionary<string, TaskCompletionSource<string>> _responseDic;

        public WebsocketSession(string host, int port, string accessToken, bool useSsl = false, ILogger? logger = null) : base(host, port, accessToken, useSsl, logger)
        {
            var wsScheme = UseSsl ? "wss" : "ws";
            _wsUri = new Uri($"{wsScheme}://{Host}:{Port}");
            _wsClient = new WebsocketClient(_wsUri);
            _wsClient.MessageReceived.Subscribe(async (msg) => await MessageReceived(msg));
            _responseDic = new ConcurrentDictionary<string, TaskCompletionSource<string>>();
        }

        protected async Task MessageReceived(ResponseMessage msg)
        {
            try
            {
                Logger?.LogTrace("[Trace][Message]{0}", msg.Text);
                var jobj = JObject.Parse(msg.Text);
                var postTypeName = jobj[Post.TypePropertyName]?.ToString();
                if (postTypeName != null)
                {
                    var subTypePropertyName = EventResolver.GetSubTypePropertyName(postTypeName);
                    var subType = jobj[subTypePropertyName]?.ToString();
                    if (subType != null)
                    {
                        var type = EventResolver.GetPostType(postTypeName, subType);
                        var post = (Post?)jobj.ToObject(type);
                        if(!(post is MetaEventPost))
                        {
                            Logger?.LogDebug("[Debug][Receive]{0}", msg.Text);
                        }
                        await InvokeHandler(post);
                        return;
                    }
                    throw new Exception($"json has no sub type, json:{jobj}");
                }
                else if (jobj[ResponsePost.MainPropertyName]?.ToString() is string echo)
                {
                    ProcessResponse(echo, msg.Text);
                    return;
                }
                throw new Exception($"json has no valid property type, json:{jobj}");
            }
            catch(Exception e)
            {
                Logger?.LogError(e, "message process error, origin message:{0}", msg?.Text);
            }
        }

        public override async Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            await _wsClient.StartOrFail();
        }

        public async ValueTask DisposeAsync()
        {
            if (_wsClient != null)
            {
                await _wsClient.Stop(System.Net.WebSockets.WebSocketCloseStatus.NormalClosure, "closed by client");
                _wsClient.Dispose();
            }
        }

        private async Task<TData> SendAsync<TData>(WebsocketRequest req) where TData : class
        {
            try
            {
                var completionSource = new TaskCompletionSource<string>();
                _responseDic.TryAdd(req.Echo, completionSource);
                var request = JsonConvert.SerializeObject(req);
                _wsClient.Send(request);
                var resJson = await completionSource.Task;
                var res = JsonConvert.DeserializeObject<ResponsePost<TData>>(resJson);
                if(res == null)
                {
                    throw new Exception("response is null");
                }
                if (!res.IsSuccess)
                {
                    throw new Exception($"{res.Msg}:{res.Wording}");
                }
                return res.Data;
            }
            finally
            {
                _responseDic.TryRemove(req.Echo, out _);
            }
        }

        private async Task SendAsync(WebsocketRequest req)
        {
            await Task.Yield();
            var request = JsonConvert.SerializeObject(req);
            _wsClient.Send(request);
        }

        private void ProcessResponse(string echo, string rawMessage)
        {
            if (_responseDic.ContainsKey(echo))
            {
                _responseDic[echo].SetResult(rawMessage);
            }
        }

        public override Task<GuildProfile> GetGuildServiceProfile()
        {
            return SendAsync<GuildProfile>(new WebsocketRequest("get_guild_service_profile"));
        }

        public override Task<List<GuildInfo>> GetGuildList()
        {
            return SendAsync<List<GuildInfo>>(new WebsocketRequest("get_guild_list"));
        }

        public override Task<GuildGuestMeta> GetGuildMetaByGuest(string guildId)
        {
            return SendAsync<GuildGuestMeta>(new WebsocketRequest<GetGuildMetaByGuestRequest>("get_guild_meta_by_guest", new GetGuildMetaByGuestRequest(guildId)));
        }

        public override Task<List<ChannelInfo>> GetGuildChannelList(string guildId, bool noCache)
        {
            return SendAsync<List<ChannelInfo>>(new WebsocketRequest<GetGuildChannelListRequest>("get_guild_channel_list", new GetGuildChannelListRequest(guildId, noCache)));
        }

        public override Task<GetGuildMemberListResponse> GetGuildMemberList(string guildId, string nextToken)
        {
            return SendAsync<GetGuildMemberListResponse>(new WebsocketRequest<GetGuildMemberListRequest>("get_guild_member_list", new GetGuildMemberListRequest(guildId, nextToken)));
        }

        public override Task<GuildMemberInfo> GetGuildMemberProfile(string guildId, string tinyId)
        {
            return SendAsync<GuildMemberInfo>(new WebsocketRequest<GetGuildMemberProfileRequest>("get_guild_member_profile", new GetGuildMemberProfileRequest(guildId, tinyId)));
        }

        public override Task<string> SendGuildChannelMsg(string guildId, string channelId, string msg)
        {
            return SendAsync<string>(new WebsocketRequest<SendGuildChannelMsgRequest>("send_guild_channel_msg", new SendGuildChannelMsgRequest(guildId, channelId, msg)));
        }

        public override Task<List<FeedInfo>> GetTopicChannelFeeds(string guildId, string channelId)
        {
            return SendAsync<List<FeedInfo>>(new WebsocketRequest<GetTopicChannelFeedsRequest>("get_topic_channel_feeds", new GetTopicChannelFeedsRequest(guildId, channelId)));
        }

        public override Task DeleteGuildRole(string guildId, string roleId)
        {
            return SendAsync(new WebsocketRequest<DeleteGuildRoleRequest>("delete_guild_role", new DeleteGuildRoleRequest(guildId, roleId)));
        }

        public override Task<List<RoleInfo>> GetGuildRoles(string guildId)
        {
            return SendAsync<List<RoleInfo>>(new WebsocketRequest<GetGuildRolesRequest>("get_topic_channel_feeds", new GetGuildRolesRequest(guildId)));
        }

        public override Task SetGuildMemberRole(string guildId, bool set, string roleId, List<string> users)
        {
            return SendAsync(new WebsocketRequest<SetGuildMemberRoleRequest>("set_guild_member_role", new SetGuildMemberRoleRequest(guildId, set, roleId, users)));
        }

        public override Task UpdateGuildRole(string guildId, string roleId, string name, string color, bool independent)
        {
            return SendAsync(new WebsocketRequest<UpdateGuildRoleRequest>("update_guild_role", new UpdateGuildRoleRequest(guildId, roleId, name, color, independent)));
        }

        public override Task<string> CreateGuildRole(string guildId, string color, string name, bool independent, List<string> initialUsers)
        {
            return SendAsync<string>(new WebsocketRequest<CreateGuildRoleRequest>("create_guild_role", new CreateGuildRoleRequest(guildId, color, name, independent, initialUsers)));
        }

        public override async Task<string> SendGroupMessage(string groupId, string message, bool autoEscape = false)
        {
            var res = await SendAsync<SendMessageResponse>(new WebsocketRequest<SendGroupMessageRequest>("send_group_msg", new SendGroupMessageRequest(groupId, message, autoEscape)));
            return res.MessageId;
        }

        public override async Task<string> SendPrivateMessage(string userId, string message, bool autoEscape = false, string? tempFromGroupId = null)
        {
            var res = await SendAsync<SendMessageResponse>(new WebsocketRequest<SendPrivateMessageRequest>("send_private_msg", new SendPrivateMessageRequest(userId, tempFromGroupId, message, autoEscape)));
            return res.MessageId;
        }

        public override Task<List<GroupMemberInfo>> GetGroupMemberList(string groupId, bool noCache = true)
        {
            return SendAsync<List<GroupMemberInfo>>(new WebsocketRequest<GetGroupMemberListRequest>("get_group_member_list", new GetGroupMemberListRequest(groupId, noCache)));
        }
    }
}
