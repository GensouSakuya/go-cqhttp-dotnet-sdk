using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models
{
    public class ReactionInfo
    {
        [JsonProperty("emoji_id")]
        public string? EmojiId { get; set; }
        [JsonProperty("emoji_index")]
        public int EmojiIndex { get; set; }
        [JsonProperty("emoji_type")]
        public int EmojiType { get; set; }
        [JsonProperty("emoji_name")]
        public string? EmojiName { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
