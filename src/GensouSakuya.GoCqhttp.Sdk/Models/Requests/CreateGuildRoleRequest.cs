using Newtonsoft.Json;
using System.Collections.Generic;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Requests
{
    internal class CreateGuildRoleRequest
    {
        public CreateGuildRoleRequest(string guildId, string name, string color, bool independent, List<string> initialUsers)
        {
            GuildId = guildId;
            Name = name;
            Color = color;
            Independent = independent;
            InitialUsers = initialUsers;
        }

        [JsonProperty("guild_id")]
        public string GuildId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("independent")]
        public bool Independent { get; set; }

        [JsonProperty("initial_users")]
        public List<string> InitialUsers { get; set; }
    }
}
