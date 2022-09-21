using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Requests
{
    internal class UpdateGuildRoleRequest
    {
        public UpdateGuildRoleRequest(string guildId, string roleId, string name, string color, bool independent)
        {
            GuildId = guildId;
            RoleId = roleId;
            Name = name;
            Color = color;
            Independent = independent;
        }

        [JsonProperty("guild_id")]
        public string GuildId { get; set; }

        [JsonProperty("role_id")]
        public string RoleId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("independent")]
        public bool Independent { get; set; }
    }
}
