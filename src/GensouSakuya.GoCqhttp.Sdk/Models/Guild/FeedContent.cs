using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Guild
{
    public class FeedContent
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("data")]
        public FeedContentData? Data { get; set; }
    }

    public class FeedContentData
    {
        //text
        [JsonProperty("text")]
        public string? Text { get; set; }

        //face\at
        [JsonProperty("id")]
        public string? Id { get; set; }

        //at
        [JsonProperty("qq")]
        public string? QQ { get; set; }

        //url_quote\channel_quote
        [JsonProperty("display_text")]
        public string? DisplayText { get; set; }

        //url_quote
        [JsonProperty("url")]
        public string? Url { get; set; }
        [JsonProperty("guild_id")]
        public string? GuildId { get; set; }
        [JsonProperty("channel_id")]
        public string? ChannelId { get; set; }

    }
}
