using GensouSakuya.GoCqhttp.Sdk.Sessions.Models.Responses.Guild;
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

namespace GensouSakuya.GoCqhttp.Sdk.Sessions
{
    public class WebsocketSession : BaseSession, IAsyncDisposable
    {
        //private static readonly EventJsonConverter _eventJsonConverter = new EventJsonConverter();
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

        public override async Task<GuildMemberInfo?> GetGuildMemberProfile(long guildId, long tinyId)
        {
            var res = await Send<ResponsePost<GuildMemberInfo>>(new WebsocketRequest<object>("get_guild_member_profile", new GetGuildMemberProfileRequest(guildId, tinyId)));
            //if (res.IsSuccess)
            return res.Data;
        }

        private async Task<TData?> Send<TData>(WebsocketRequest req) where TData : ResponsePost
        {
            var completionSource = new TaskCompletionSource<string>();
            _responseDic.TryAdd(req.Echo, completionSource);
            var request = JsonConvert.SerializeObject(req);
            _wsClient.Send(request);
            var resJson = await completionSource.Task;
            var res = JsonConvert.DeserializeObject<TData>(resJson);
            return res;
        }

        private void ProcessResponse(string echo, string rawMessage)
        {
            if (_responseDic.ContainsKey(echo))
            {
                _responseDic[echo].SetResult(rawMessage);
            }
        }
    }
}
