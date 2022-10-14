using System;
using System.Threading;
using System.Threading.Tasks;
using GensouSakuya.GoCqhttp.Sdk.Sessions.Drivers.Events;
using GensouSakuya.GoCqhttp.Sdk.Sessions.Models.PostEvents.Base;
using Newtonsoft.Json;
using Websocket.Client;
using System.Collections.Concurrent;
using GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Base;
using GensouSakuya.GoCqhttp.Sdk.Models.Requests;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using GensouSakuya.GoCqhttp.Sdk.Models.Guild;
using System.Linq;
using GensouSakuya.GoCqhttp.Sdk.Models.Responses;

namespace GensouSakuya.GoCqhttp.Sdk.Sessions
{
    public class WebsocketSession : BaseSession, IAsyncDisposable
    {
        private Uri _wsUri;
        private WebsocketClient _wsClient;
        private ConcurrentDictionary<string, TaskCompletionSource<string>> _responseDic;

        public WebsocketSession(string host, int port, string accessToken, bool useSsl = false) : base(host, port, accessToken, useSsl)
        {
            var wsScheme = UseSsl ? "wss" : "ws";
            _wsUri = new Uri($"{wsScheme}://{Host}:{Port}");
            _wsClient = new WebsocketClient(_wsUri);
            _wsClient.MessageReceived.Subscribe(async (msg) => await MessageReceived(msg));
            _responseDic = new ConcurrentDictionary<string, TaskCompletionSource<string>>();
        }

        protected async Task MessageReceived(ResponseMessage msg)
        {
#if DEBUG
            Console.WriteLine(msg.Text);
#endif
            var jobj = JObject.Parse(msg.Text);
            if (jobj[Post.TypePropertyName] != null)
            {
                var postType = jobj[Post.TypePropertyName].ToString();
                var subTypePropertyName = EventResolver.GetSubTypePropertyName(postType);
                if (jobj[subTypePropertyName] != null)
                {
                    var subType = jobj[subTypePropertyName].ToString();
                    var type = EventResolver.GetPostType(postType, subType);
                    var post = (Post)jobj.ToObject(type);
                    await InvokeHandler(post);
                    return;
                }
                throw new Exception($"json has no sub type, json:{jobj}");
            }
            else if (jobj[ResponsePost.MainPropertyName] != null)
            {
                var echo = jobj[ResponsePost.MainPropertyName].ToString();
                ProcessResponse(echo,msg.Text);
                return;
            }
            throw new Exception($"json has no valid property type, json:{jobj}");
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

        private async Task<TData> SendAsync<TData>(WebsocketRequest req) where TData : ResponsePost
        {
            try
            {
                var completionSource = new TaskCompletionSource<string>();
                _responseDic.TryAdd(req.Echo, completionSource);
                var request = JsonConvert.SerializeObject(req);
                _wsClient.Send(request);
                var resJson = await completionSource.Task;
                var res = JsonConvert.DeserializeObject<TData>(resJson);
                if(res == null)
                {
                    throw new Exception("response is null");
                }
                if (!res.IsSuccess)
                {
                    throw new Exception($"{res.Msg}:{res.Wording}");
                }
                return res;
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

        public override async Task<GuildProfile?> GetGuildServiceProfile()
        {
            var res = await SendAsync<ResponsePost<GuildProfile>>(new WebsocketRequest("get_guild_service_profile"));
            return res.Data;
        }

        public override async Task<List<GuildInfo>?> GetGuildList()
        {
            var res = await SendAsync<ResponsePost<List<GuildInfo>>>(new WebsocketRequest("get_guild_list"));
            return res.Data;
        }

        public override async Task<GuildGuestMeta?> GetGuildMetaByGuest(string guildId)
        {
            var res = await SendAsync<ResponsePost<GuildGuestMeta>>(new WebsocketRequest<GetGuildMetaByGuestRequest>("get_guild_meta_by_guest", new GetGuildMetaByGuestRequest(guildId)));
            return res.Data;
        }

        public override async Task<List<ChannelInfo>?> GetGuildChannelList(string guildId, bool noCache)
        {
            var res = await SendAsync<ResponsePost<List<ChannelInfo>>>(new WebsocketRequest<GetGuildChannelListRequest>("get_guild_channel_list", new GetGuildChannelListRequest(guildId, noCache)));
            return res.Data;
        }

        public override async Task<GetGuildMemberListResponse> GetGuildMemberList(string guildId, string nextToken)
        {
            var res = await SendAsync<ResponsePost<GetGuildMemberListResponse>>(new WebsocketRequest<GetGuildMemberListRequest>("get_guild_member_list", new GetGuildMemberListRequest(guildId, nextToken)));
            return res.Data;
        }

        public override async Task<GuildMemberInfo?> GetGuildMemberProfile(string guildId, string tinyId)
        {
            var res = await SendAsync<ResponsePost<GuildMemberInfo>>(new WebsocketRequest<GetGuildMemberProfileRequest>("get_guild_member_profile", new GetGuildMemberProfileRequest(guildId, tinyId)));
            return res.Data;
        }

        public override async Task<string?> SendGuildChannelMsg(string guildId, string channelId, string msg)
        {
            var res = await SendAsync<ResponsePost<string>>(new WebsocketRequest<SendGuildChannelMsgRequest>("send_guild_channel_msg", new SendGuildChannelMsgRequest(guildId, channelId, msg)));
            return res.Data;
        }

        public override async Task<List<FeedInfo>?> GetTopicChannelFeeds(string guildId, string channelId)
        {
            var res = await SendAsync<ResponsePost<List<FeedInfo>>>(new WebsocketRequest<GetTopicChannelFeedsRequest>("get_topic_channel_feeds", new GetTopicChannelFeedsRequest(guildId, channelId)));
            return res.Data;
        }

        public override async Task DeleteGuildRole(string guildId, string roleId)
        {
            await SendAsync(new WebsocketRequest<DeleteGuildRoleRequest>("delete_guild_role", new DeleteGuildRoleRequest(guildId, roleId)));
        }

        public override async Task<List<RoleInfo>> GetGuildRoles(string guildId)
        {
            var res = await SendAsync<ResponsePost<List<RoleInfo>>>(new WebsocketRequest<GetGuildRolesRequest>("get_topic_channel_feeds", new GetGuildRolesRequest(guildId)));
            return res.Data ?? Enumerable.Empty<RoleInfo>().ToList();
        }

        public override async Task SetGuildMemberRole(string guildId, bool set, string roleId, List<string> users)
        {
            await SendAsync(new WebsocketRequest<SetGuildMemberRoleRequest>("set_guild_member_role", new SetGuildMemberRoleRequest(guildId, set, roleId, users)));
        }

        public override async Task UpdateGuildRole(string guildId, string roleId, string name, string color, bool independent)
        {
            await SendAsync(new WebsocketRequest<UpdateGuildRoleRequest>("update_guild_role", new UpdateGuildRoleRequest(guildId, roleId, name, color, independent)));
        }

        public override async Task<string> CreateGuildRole(string guildId, string color, string name, bool independent, List<string> initialUsers)
        {
            var res = await SendAsync<ResponsePost<string>>(new WebsocketRequest<CreateGuildRoleRequest>("create_guild_role", new CreateGuildRoleRequest(guildId, color, name, independent, initialUsers)));
            return res.Data;
        }
    }
}
