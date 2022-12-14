namespace GensouSakuya.GoCqhttp.Sdk.Models.Messages
{
    public class UnknownMessage : BaseMessage
    {
        public UnknownMessage(string rawText)
        {
        }

        protected override string GenerateRawText()
        {
            return RawText;
        }
    }
}
