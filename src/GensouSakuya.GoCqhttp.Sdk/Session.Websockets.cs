using System;
using System.Threading;
using System.Threading.Tasks;
using Websocket.Client;

namespace GensouSakuya.GoCqhttp.Sdk
{
    public partial class Session
    {
        private Uri _wsUri;
        private WebsocketClient _wsClient;

        private async Task WebsocketConnectAsync(CancellationToken cancellationToken = default)
        {
            await _wsClient.StartOrFail();
        }
    }
}
