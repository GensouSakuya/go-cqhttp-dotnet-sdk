using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Guild
{
    public class RoleInfo
    {
        [JsonProperty("role_id")]
        public string? RoleId { get; set; }

        [JsonProperty("role_name")]
        public string? RoleName { get; set; }

        [JsonProperty("argb_color")]
        public long ArgbColor { get; set; }

        [JsonProperty("disabled")]
        public bool? Disabled { get; set; }

        [JsonProperty("independent")]
        public bool independent { get; set; }

        [JsonProperty("max_count")]
        public int? MaxCount { get; set; }

        [JsonProperty("member_count")]
        public int? MemberCount { get; set; }

        [JsonProperty("owned")]
        public bool Owned { get; set; }
    }
}
