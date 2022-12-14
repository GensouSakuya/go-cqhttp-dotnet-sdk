using GensouSakuya.GoCqhttp.Sdk.Drivers.Messages;
using Newtonsoft.Json;
using System.Text;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Messages
{
    [CQ("face")]
    public class FaceMessage : BaseMessage
    {
        public FaceMessage(string id)
        {
            Id = id;
        }

        internal FaceMessage() { }

        [JsonProperty("id")]
        public string? Id { get; set; }

        protected override string GenerateRawText()
        {
            return $"[CQ:face,id={Id}]";
        }
    }
}
