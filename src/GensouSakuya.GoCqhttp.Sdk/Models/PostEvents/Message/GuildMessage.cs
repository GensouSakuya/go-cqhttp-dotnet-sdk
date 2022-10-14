using GensouSakuya.GoCqhttp.Sdk.Sessions.Drivers.Events;
using GensouSakuya.GoCqhttp.Sdk.Sessions.Models.PostEvents.Base;
using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Sessions.Models.PostEvents.Message
{
    [PostSubType("guild")]
    public class GuildMessage : MessagePost
    {
        [JsonProperty("guild_id")]
        public string? GuildId { get; set; }

        [JsonProperty("channel_id")]
        public string? ChannelId { get; set; }

        public string TinyId => UserId;
    }
}
