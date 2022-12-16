using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Requests
{
    internal class SendGuildChannelMsgRequest
    {
        public SendGuildChannelMsgRequest(string guildId, string? channelId, string message)
        {
            GuildId = guildId;
            ChannelId = channelId;
            Message = message;
        }

        [JsonProperty("guild_id")]
        public string GuildId { get; set; }

        [JsonProperty("channel_id")]
        public string? ChannelId { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
