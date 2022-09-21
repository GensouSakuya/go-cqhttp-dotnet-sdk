using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Guild
{
    public class GuildGuestMeta
    {
        [JsonProperty("guild_id")]
        public string? GuildId { get; set; }

        [JsonProperty("guild_name")]
        public string? GuildName { get; set; }

        [JsonProperty("guild_profile")]
        public string? GuildProfile { get; set; }

        [JsonProperty("create_time")]
        public long CreateTime { get; set; }

        [JsonProperty("max_member_count")]
        public long MaxMemberCount { get; set; }

        [JsonProperty("max_robot_count")]
        public long MaxRobotCount { get; set; }

        [JsonProperty("max_admin_count")]
        public long MaxAdminCount { get; set; }

        [JsonProperty("member_count")]
        public long MemberCount { get; set; }

        [JsonProperty("owner_id")]
        public string? OwnerId { get; set; }
    }
}
