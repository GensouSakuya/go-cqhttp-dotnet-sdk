using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Guild
{
    public class GuildProfile
    {
        [JsonProperty("nickname")]
        public string? NickName { get; set; }

        [JsonProperty("tiny_id")]
        public string? TinyId { get; set; }

        [JsonProperty("avatar_url")]
        public string? AvatarUrl { get; set; }
    }
}
