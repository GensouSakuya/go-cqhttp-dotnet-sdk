using GensouSakuya.GoCqhttp.Sdk.Drivers.Messages;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Messages
{
    [CQ("at")]
    public class AtMessage : BaseMessage
    {
        public AtMessage(string qq)
        {
            QQ = qq;
        }

        public string QQ { get; set; }

        internal AtMessage() { }

        protected override string GenerateRawText()
        {
            return $"[CQ:at,qq={QQ}]";
        }
    }
}
