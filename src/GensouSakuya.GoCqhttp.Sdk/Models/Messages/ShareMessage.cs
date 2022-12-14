using GensouSakuya.GoCqhttp.Sdk.Drivers.Messages;
using Newtonsoft.Json;
using System.Text;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Messages
{
    [CQ("share")]
    public class ShareMessage : BaseMessage
    {
        public ShareMessage(string? url, string? title, string? content, string? image)
        {
            Url = url;
            Title = title;
            Content = content;
            Image = image;
        }

        internal ShareMessage() { }

        [JsonProperty("url")]
        public string? Url { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        public string? Content { get; set; }
        
        public string? Image { get; set; }

        protected override string GenerateRawText()
        {
            var builder = new StringBuilder("[CQ:share");
            builder.Append($",url={Url}");
            builder.Append($",title={Title}");
            if (!string.IsNullOrEmpty(Content))
            {
                builder.Append($",file={Content}");
            }
            if (!string.IsNullOrEmpty(Image))
            {
                builder.Append($",subType={Image}");
            }
            builder.Append("]");
            return builder.ToString();
        }
    }
}
