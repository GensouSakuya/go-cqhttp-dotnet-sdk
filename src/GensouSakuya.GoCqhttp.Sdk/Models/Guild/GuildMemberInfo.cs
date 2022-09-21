using Newtonsoft.Json;
using System.Collections.Generic;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Guild
{
    public class GuildMemberInfo
    {
        [JsonProperty("tiny_id")]
        public string? TinyId { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("nickname")]
        public string? NickName { get; set; }

        //非全部，仅最新角色
        [JsonProperty("role_id")]
        public string? RoleId { get; set; }
        [JsonProperty("role_name")]
        public string? RoleName { get; set; }

        [JsonProperty("avatar_url")]
        public string? AvatarUrl { get; set; }

        [JsonProperty("join_time")]
        public long? JoinTime { get; set; }

        [JsonProperty("roles")]
        public List<RoleInfo>? Roles { get; set; }
    }
}
