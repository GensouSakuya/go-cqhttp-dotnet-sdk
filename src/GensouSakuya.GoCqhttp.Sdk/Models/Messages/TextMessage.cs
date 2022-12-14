namespace GensouSakuya.GoCqhttp.Sdk.Models.Messages
{
    public class TextMessage : BaseMessage
    {
        public TextMessage(string text)
        {
            Text = text;
        }

        public string Text { get; internal set; }

        protected override string GenerateRawText()
        {
            return Text;
        }
    }
}
