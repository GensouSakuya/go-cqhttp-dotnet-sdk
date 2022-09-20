using GensouSakuya.GoCqhttp.Sdk.Drivers.Events;
using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Base
{
    [PostInfo("request", "request_type")]
    public abstract class RequestPost : Post
    {
        [JsonProperty("request_type")]
        public string? RequestType { get; set; }
    }
}
