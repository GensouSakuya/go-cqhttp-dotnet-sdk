using Newtonsoft.Json;
using System;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Requests
{
    internal class WebsocketRequest
    {
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("echo")]
        public string Echo { get; set; }

        public WebsocketRequest(string action)
        {
            Action = action;
            Echo = Guid.NewGuid().ToString();
        }
    }

    internal class WebsocketRequest<T> : WebsocketRequest
    {
        public WebsocketRequest(string action, T param) : base(action)
        {
            Params = param;
        }

        [JsonProperty("params")]
        public T Params { get; set; }
    }
}
