using GensouSakuya.GoCqhttp.Sdk.Sessions.Drivers.Events;
using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Sessions.Models.PostEvents.Base
{
    [PostInfo("meta_event", "meta_event_type")]
    public abstract class MetaEventPost : Post
    {
        [JsonProperty("meta_event_type")]
        public string? MetaEventType { get; set; }
    }
}
