using GensouSakuya.GoCqhttp.Sdk.Drivers.Messages;
using GensouSakuya.GoCqhttp.Sdk.Models.Messages;
using GensouSakuya.GoCqhttp.Sdk.Sessions.Drivers.Events;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace GensouSakuya.GoCqhttp.Sdk.Sessions.Models.PostEvents.Base
{
    [PostInfo("message", "message_type")]
    public abstract class MessagePost : Post
    {
        [JsonProperty("sub_type")]
        public string? SubType { get; set; }

        [JsonProperty("message_type")]
        public string? MessageType { get; set; }

        [JsonProperty("message_id")]
        public string? MessageId { get; set; }

        [JsonProperty("user_Id")]
        public string? UserId { get; set; }

        [JsonProperty("message")]
        public object? Message { get; set; }

        [JsonProperty("raw_message")]
        public string? RawMessage { get; set; }

        [JsonProperty("font")]
        public int Font { get; set; }

        [JsonProperty("sender")]
        public object? Sender { get; set; }

        private IEnumerable<BaseMessage>? _messageChain;
        public IEnumerable<BaseMessage> MessageChain
        {
            get
            {
                if (_messageChain == null)
                    _messageChain = MessageChainConverter.ConvertToChain(RawMessage);
                return _messageChain;
            }
            internal set {
                _messageChain = value; 
            }
        }
    }
}
