using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Requests
{
    internal class SendPrivateMessageRequest
    {
        public SendPrivateMessageRequest(string userId, string? groupId, string message, bool autoEscape)
        {
            UserId = userId;
            GroupId = groupId;
            Message = message;
            AutoEscape = autoEscape;
        }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("group_id")]
        public string? GroupId { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("auto_escape")]
        public bool AutoEscape { get; set; }
    }
}
