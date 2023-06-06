using GensouSakuya.GoCqhttp.Sdk.Drivers.Messages;
using System;
using System.Net;
using System.Text;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Messages
{
    [CQ("json")]
    public class JsonMessage : BaseMessage
    {
        public JsonMessage(string json)
        {
            Data = json;
            _dataNCRDecorded = new Lazy<string>(()=> WebUtility.HtmlDecode(Data));
        }

        public string? Data { get; set; }

        private readonly Lazy<string> _dataNCRDecorded;
        public string DataNCRDecorded
        {
            get => _dataNCRDecorded.Value;
        }

        protected override string GenerateRawText()
        {
            var builder = new StringBuilder("[CQ:json");
            if (!string.IsNullOrEmpty(Data))
            {
                builder.Append($",json={Data}");
            }
            builder.Append("]");
            return builder.ToString();
        }
    }
}
