namespace GensouSakuya.GoCqhttp.Sdk.Models.Messages
{
    public abstract class BaseMessage
    {
        private string? _rawText;
        public string RawText
        {
            get
            { 
                if (string.IsNullOrEmpty(_rawText))
                {
                    _rawText = GenerateRawText();
                }
                return _rawText;
            }
            set => _rawText = value;
        }

        internal BaseMessage() { }

        protected abstract string GenerateRawText();
    }
}
