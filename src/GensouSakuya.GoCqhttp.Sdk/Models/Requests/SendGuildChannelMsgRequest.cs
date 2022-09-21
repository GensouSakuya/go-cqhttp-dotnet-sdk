using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Requests
{
    internal class SendGuildChannelMsgRequest
    {
        public SendGuildChannelMsgRequest(string guildId, string? nextToken, string message)
        {
            GuildId = guildId;
            NextToken = nextToken;
            Message = message;
        }

        [JsonProperty("guild_id")]
        public string GuildId { get; set; }

        [JsonProperty("next_token")]
        public string? NextToken { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
