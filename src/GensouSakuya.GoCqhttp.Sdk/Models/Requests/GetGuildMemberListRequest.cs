using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Requests
{
    internal class GetGuildMemberListRequest
    {
        public GetGuildMemberListRequest(string guildId, string? nextToken)
        {
            GuildId = guildId;
            NextToken = nextToken;
        }

        [JsonProperty("guild_id")]
        public string GuildId { get; set; }

        [JsonProperty("next_token")]
        public string? NextToken { get; set; }
    }
}
