using Newtonsoft.Json;
using System.Collections.Generic;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Responses.Guild
{
    public class GuildMemberInfo
    {
        [JsonProperty("tiny_id")]
        public long? TinyId { get; set; }

        [JsonProperty("nickname")]
        public string? NickName { get; set; }

        [JsonProperty("avatar_url")]
        public string? AvatarUrl { get; set; }

        [JsonProperty("join_time")]
        public long? JoinTime { get; set; }

        [JsonProperty("roles")]
        public List<RoleInfo>? Roles { get; set; }
    }
}
