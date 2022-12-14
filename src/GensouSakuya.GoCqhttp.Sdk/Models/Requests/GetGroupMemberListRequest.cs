using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Requests
{
    internal class GetGroupMemberListRequest
    {
        public GetGroupMemberListRequest(string groupId, bool noCache)
        {
            GroupId = groupId;
            NoCache = noCache;
        }

        [JsonProperty("group_id")]
        public string GroupId { get; set; }

        [JsonProperty("no_cache")]
        public bool NoCache { get; set; }
    }
}
