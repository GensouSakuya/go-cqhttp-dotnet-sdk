using GensouSakuya.GoCqhttp.Sdk.Drivers.Events;
using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Base
{
    [PostInfo("meta_event", "meta_event_type")]
    public abstract class MetaEventPost : Post
    {
        [JsonProperty("meta_event_type")]
        public string? MetaEventType { get; set; }
    }
}
