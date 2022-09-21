using GensouSakuya.GoCqhttp.Sdk.Sessions.Drivers.Events;
using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Sessions.Models.PostEvents.Base
{
    [PostInfo("request", "request_type")]
    public abstract class RequestPost : Post
    {
        [JsonProperty("request_type")]
        public string? RequestType { get; set; }
    }
}
