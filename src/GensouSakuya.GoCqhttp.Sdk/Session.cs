using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using Websocket.Client;

namespace GensouSakuya.GoCqhttp.Sdk
{
    public class Session :IAsyncDisposable 
    {
        private Uri _uri;
        private string _accessToken;
        private WebsocketClient _client;

        public Session(string host, int port, string accessToken)
        {
            _uri = new Uri($"{host}:{port}");
            _accessToken = accessToken;
            _client = new WebsocketClient(_uri);
            _client.MessageReceived.Subscribe(async (msg) => await MessageReceived(msg));
            //JsonConvert.DefaultSettings = () =>
            //{
            //    return new JsonSerializerSettings()
            //    {

            //    };
            //};
            //JsonConvert.DeserializeObject<Session>(,)
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
        }

        public async ValueTask DisposeAsync()
        {
            if(_client != null)
            {
                await _client.Stop(System.Net.WebSockets.WebSocketCloseStatus.NormalClosure, "closed by client");
                _client.Dispose();
            }
        }
    }
}
