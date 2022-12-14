using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Requests
{
    internal class SendGroupMessageRequest
    {
        public SendGroupMessageRequest(string groupId, string message, bool autoEscape)
        {
            GroupId = groupId;
            Message = message;
            AutoEscape = autoEscape;
        }

        [JsonProperty("group_id")]
        public string GroupId { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("auto_escape")]
        public bool AutoEscape { get; set; }
    }
}
