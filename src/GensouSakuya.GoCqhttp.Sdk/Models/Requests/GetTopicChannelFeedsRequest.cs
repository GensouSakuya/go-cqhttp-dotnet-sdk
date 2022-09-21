using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Requests
{
    internal class GetTopicChannelFeedsRequest
    {
        public GetTopicChannelFeedsRequest(string guildId, string channelId)
        {
            GuildId = guildId;
            ChannelId = channelId;
        }

        [JsonProperty("guild_id")]
        public string GuildId { get; set; }

        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }
    }
}
