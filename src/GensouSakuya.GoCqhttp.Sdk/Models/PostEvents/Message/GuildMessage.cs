using GensouSakuya.GoCqhttp.Sdk.Drivers.Events;
using GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Base;
using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Message
{
    [PostSubType("guild")]
    public class GuildMessage : MessagePost
    {
        [JsonProperty("guild_id")]
        public long GuildId { get; set; }

        [JsonProperty("channel_id")]
        public long ChannelId { get; set; }

        [JsonProperty("tiny_id")]
        public long TinyId { get; set; }
    }
}
