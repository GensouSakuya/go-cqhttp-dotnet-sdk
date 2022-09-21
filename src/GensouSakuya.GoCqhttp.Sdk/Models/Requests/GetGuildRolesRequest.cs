using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Requests
{
    internal class GetGuildRolesRequest
    {
        public GetGuildRolesRequest(string guildId)
        {
            GuildId = guildId;
        }

        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
    }
}
