using GensouSakuya.GoCqhttp.Sdk.Sessions.Drivers.Events;
using GensouSakuya.GoCqhttp.Sdk.Sessions.Models.PostEvents.Base;
using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Sessions.Models.PostEvents.Message
{
    [PostSubType("group")]
    public class GroupMessage : MessagePost
    {
        [JsonProperty("group_id")]
        public long GroupId { get; set; }

        [JsonProperty("anonymous")]
        public object? Anonymous { get; set; }
    }
}
