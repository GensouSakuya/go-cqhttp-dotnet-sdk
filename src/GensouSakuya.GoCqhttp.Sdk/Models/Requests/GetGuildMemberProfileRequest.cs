using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Requests
{
    internal class GetGuildMemberProfileRequest
    {
        public GetGuildMemberProfileRequest(string? guildId, string? tinyId)
        {
            GuildId = guildId;
            TinyId = tinyId;
        }

        [JsonProperty("guild_id")]
        public string? GuildId { get; set; }
        [JsonProperty("user_id")]
        public string? TinyId { get; set; }
    }
}
