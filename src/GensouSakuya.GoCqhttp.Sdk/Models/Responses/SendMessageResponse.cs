using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Responses
{
    internal class SendMessageResponse
    {
        public SendMessageResponse() { }

        [JsonProperty("message_id")]
        public string MessageId { get; set; }
    }
}
