using GensouSakuya.GoCqhttp.Sdk.Sessions.Drivers.Events;
using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Notice
{
    [PostSubType("guild_channel_recall")]
    public class GuildChannelRecallNotice : GuildNotice
    {
        [JsonProperty("operator_id")]
        public string? OperatorId { get; set; }

        [JsonProperty("message_id")]
        public string? MessageId { get; set; }
    }
}
