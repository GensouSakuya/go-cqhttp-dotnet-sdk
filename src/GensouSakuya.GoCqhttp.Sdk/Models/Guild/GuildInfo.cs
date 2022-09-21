using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Guild
{
    public class GuildInfo
    {
        [JsonProperty("guild_id")]
        public string? GuildId { get; set; }

        [JsonProperty("guild_name")]
        public string? GuildName { get; set; }

        [JsonProperty("avatar_url")]
        public long GuildDisplayId { get; set; }
    }
}
