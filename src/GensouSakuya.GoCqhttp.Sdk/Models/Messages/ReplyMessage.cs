using GensouSakuya.GoCqhttp.Sdk.Drivers.Messages;
using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Messages
{
    [CQ("reply")]
    public class ReplyMessage : BaseMessage
    {
        public ReplyMessage(string messageId)
        {
            MessageId = messageId;
        }

        [JsonProperty("id")]
        public string MessageId { get; set; }

        internal ReplyMessage() { }

        protected override string GenerateRawText()
        {
            return $"[CQ:reply,id={MessageId}]";
        }
    }
}
