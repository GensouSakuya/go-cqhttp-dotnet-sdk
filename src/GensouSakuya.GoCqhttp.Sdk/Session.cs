using GensouSakuya.GoCqhttp.Sdk.Drivers.Events;
using GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Base;
using GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Message;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
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

        private bool _useSsl;
        private string _host;
        private int _port;
        private string _accessToken;

        public Session(string host,
                       int port,
                       string accessToken,
                       bool useSsl = false)
        {
            _useSsl = useSsl;
            _host = host;
            _port = port;
            _accessToken = accessToken;

            #region websocket
            var wsScheme = _useSsl ? "wss" : "ws";
            _wsUri = new Uri($"{wsScheme}://{_host}:{_port}");
            _wsClient = new WebsocketClient(_wsUri);
            _wsClient.MessageReceived.Subscribe(async (msg) => await MessageReceived(msg));
            #endregion

            #region http
            var httpScheme = _useSsl ? "https" : "http";
            _httpUri = new Uri($"{httpScheme}://{_host}:{_port}");
            _httpClient = new RestClient(_httpUri);
            #endregion
        }

        public async Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            await WebsocketConnectAsync(cancellationToken);
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
            if(_wsClient != null)
            {
                await _wsClient.Stop(System.Net.WebSockets.WebSocketCloseStatus.NormalClosure, "closed by client");
                _wsClient.Dispose();
            }
        }

        public AsyncEventHandler<PrivateMessage>? PrivateMessageReceived { get; set; }
        public AsyncEventHandler<GroupMessage>? GroupMessageReceived { get; set; }
        public AsyncEventHandler<GuildMessage>? GuildMessageReceived { get; set; }
    }
}
