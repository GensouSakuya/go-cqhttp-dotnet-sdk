using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Guild
{
    public class FeedMedia
    {
        [JsonProperty("file_id")]
        public string? FileId { get; set; }

        [JsonProperty("pattern_id")]
        public string? PatternId { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }
}
