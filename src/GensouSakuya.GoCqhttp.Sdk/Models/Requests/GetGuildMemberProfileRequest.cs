using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Requests
{
    public class GetGuildMemberProfileRequest
    {
        public GetGuildMemberProfileRequest(long guildId, long tinyId)
        {
            GuildId = guildId;
            TinyId = tinyId;
        }

        [JsonProperty("guild_id")]
        public long GuildId { get; set; }
        [JsonProperty("user_id")]
        public long TinyId { get; set; }
    }
}
