using Newtonsoft.Json;
using System.Collections.Generic;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Guild
{
    public class FeedInfo
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("channel_id")]
        public string? ChannelId { get; set; }

        [JsonProperty("guild_id")]
        public string? GuildId { get; set; }

        [JsonProperty("create_time")]
        public long CreateTime { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("sub_title")]
        public string? SubTitle { get; set; }

        [JsonProperty("poster_info")]
        public PosterInfo? PosterInfo { get; set; }

        [JsonProperty("resource")]
        public ResourceInfo? Resource { get; set; }

        [JsonProperty("contents")]
        public List<FeedContent> Contents { get; set; }
    }
}
