using GensouSakuya.GoCqhttp.Sdk.Drivers.Events;
using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Base
{
    [PostInfo("message", "message_type")]
    public abstract class MessagePost : Post
    {
        [JsonProperty("sub_type")]
        public string? SubType { get; set; }

        [JsonProperty("message_type")]
        public string? MessageType { get; set; }

        [JsonProperty("message_id")]
        public string MessageId { get; set; }

        [JsonProperty("user_Id")]
        public long UserId { get; set; }

        [JsonProperty("message")]
        public object? Message { get; set; }

        [JsonProperty("raw_message")]
        public string? RawMessage { get; set; }

        [JsonProperty("font")]
        public int Font { get; set; }

        [JsonProperty("sender")]
        public object? Sender { get; set; }
    }
}
