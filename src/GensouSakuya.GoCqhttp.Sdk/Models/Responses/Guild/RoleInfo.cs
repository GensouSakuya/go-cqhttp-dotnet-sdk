using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Responses.Guild
{
    public class RoleInfo
    {
        [JsonProperty("role_id")]
        public string? RoleId { get; set; }

        [JsonProperty("role_name")]
        public string? RoleName { get; set; }
    }
}
