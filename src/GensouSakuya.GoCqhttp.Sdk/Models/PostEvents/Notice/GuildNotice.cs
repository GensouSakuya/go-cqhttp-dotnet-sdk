using GensouSakuya.GoCqhttp.Sdk.Sessions.Models.PostEvents.Base;
using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Notice
{
    public abstract class GuildNotice:NoticePost
    {
        [JsonProperty("guild_id")]
        public string? GuildId { get; set; }

        [JsonProperty("channel_id")]
        public string? ChannelId { get; set; }

        [JsonProperty("user_id")]
        public string? UserId { get; set; }
    }
}
