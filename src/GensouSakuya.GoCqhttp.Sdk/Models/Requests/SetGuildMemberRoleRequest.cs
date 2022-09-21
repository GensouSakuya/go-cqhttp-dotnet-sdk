using Newtonsoft.Json;
using System.Collections.Generic;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Requests
{
    internal class SetGuildMemberRoleRequest
    {
        public SetGuildMemberRoleRequest(string guildId, bool set, string roleId, List<string> users)
        {
            GuildId = guildId;
            Set = set;
            RoleId = roleId;
            Users = users;
        }

        [JsonProperty("guild_id")]
        public string GuildId { get; set; }

        [JsonProperty("set")]
        public bool Set { get; set; }

        [JsonProperty("role_id")]
        public string RoleId { get; set; }

        [JsonProperty("users")]
        public List<string> Users { get; set; }
    }
}
