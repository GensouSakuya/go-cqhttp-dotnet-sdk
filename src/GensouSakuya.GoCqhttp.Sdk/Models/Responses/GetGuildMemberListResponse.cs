using GensouSakuya.GoCqhttp.Sdk.Models.Guild;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Responses
{
    public class GetGuildMemberListResponse
    {
        [JsonProperty("members")]
        public List<GuildMemberInfo>? Members { get; set; }
        [JsonProperty("finished")]
        public bool IsFinished { get; set; }
        [JsonProperty("next_token")]
        public string? NextToken { get; set; }
    }
}
