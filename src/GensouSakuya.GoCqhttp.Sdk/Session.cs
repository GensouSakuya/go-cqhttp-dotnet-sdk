using GensouSakuya.GoCqhttp.Sdk.Drivers.Events;
using GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Base;
using GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Message;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using Websocket.Client;

namespace GensouSakuya.GoCqhttp.Sdk
{
    public partial class Session :IAsyncDisposable
    {
        #region Static
        private static readonly EventJsonConverter _eventJsonConverter = new EventJsonConverter();
        static Session()
        {
            PrepareHandlerAutoMap();
        }
        #endregion

        private Uri _uri;
        private string _accessToken;
        private WebsocketClient _client;

        public Session(string host, int port, string accessToken, bool useSsl = false)
        {
            var scheme = useSsl ? "wss" : "ws";
            _uri = new Uri($"{scheme}://{host}:{port}");
            _accessToken = accessToken;
            _client = new WebsocketClient(_uri);
            _client.MessageReceived.Subscribe(async (msg) => await MessageReceived(msg));
        }

        public async Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            await _client.StartOrFail();
        }

        private async Task MessageReceived(ResponseMessage msg)
        {
#if DEBUG
            Console.WriteLine(msg.Text);
#endif
            var post = JsonConvert.DeserializeObject<Post>(msg.Text, _eventJsonConverter);
            if (post != null)
                await InvokeHandler(post);
        }

        public async ValueTask DisposeAsync()
        {
            if(_client != null)
            {
                await _client.Stop(System.Net.WebSockets.WebSocketCloseStatus.NormalClosure, "closed by client");
                _client.Dispose();
            }
        }

        public AsyncEventHandler<PrivateMessage>? PrivateMessageReceived { get; set; }
        public AsyncEventHandler<GroupMessage>? GroupMessageReceived { get; set; }
        public AsyncEventHandler<GuildMessage>? GuildMessageReceived { get; set; }
    }
}
