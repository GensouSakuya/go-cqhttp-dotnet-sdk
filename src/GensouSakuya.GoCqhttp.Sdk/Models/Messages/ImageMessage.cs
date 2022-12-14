using GensouSakuya.GoCqhttp.Sdk.Drivers.Messages;
using Newtonsoft.Json;
using System.Text;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Messages
{
    [CQ("image")]
    public class ImageMessage:BaseMessage
    {
        public ImageMessage(string? file, int subType, string? url)
        {
            this.File = file;
            this.SubType = subType;
            this.Url = url;
        }

        internal ImageMessage() { }

        /// <summary>
        /// 图片路径必须为与Go-cqhttp的相对路径
        /// </summary>
        [JsonProperty("file")]
        public string? File { get; set; }

        /// <summary>
        /// 0:正常图片;1:表情包;2:热图;...
        /// </summary>
        [JsonProperty("subType")]
        public int SubType { get; set; }
        [JsonProperty("url")]
        public string? Url { get; set; }

        protected override string GenerateRawText()
        {
            var builder = new StringBuilder("[CQ:image");
            if (!string.IsNullOrEmpty(File))
            {
                builder.Append($",file={File}");
            }
            builder.Append($",subType={SubType}");
            if (!string.IsNullOrEmpty(Url))
            {
                builder.Append($",url={Url}");
            }
            builder.Append("]");
            return builder.ToString();
        }
    }
}
