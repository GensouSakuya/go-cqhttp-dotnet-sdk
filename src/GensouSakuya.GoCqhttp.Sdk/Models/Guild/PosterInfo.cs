using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Guild
{
    public class PosterInfo
    {
        [JsonProperty("tiny_id")]
        public string? TinyId { get; set; }

        [JsonProperty("nickname")]
        public string? Nickname { get; set; }

        [JsonProperty("icon_url")]
        public string? IconUrl { get; set; }
    }
}
