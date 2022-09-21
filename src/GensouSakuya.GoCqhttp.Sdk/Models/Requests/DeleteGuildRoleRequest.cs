using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Requests
{
    internal class DeleteGuildRoleRequest
    {
        public DeleteGuildRoleRequest(string guildId, string roleId)
        {
            GuildId = guildId;
            RoleId = roleId;
        }

        [JsonProperty("guild_id")]
        public string GuildId { get; set; }

        [JsonProperty("role_id")]
        public string RoleId { get; set; }
    }
}
