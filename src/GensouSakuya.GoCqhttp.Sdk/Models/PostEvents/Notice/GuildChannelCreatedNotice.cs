using GensouSakuya.GoCqhttp.Sdk.Models.Guild;
using GensouSakuya.GoCqhttp.Sdk.Sessions.Drivers.Events;
using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Notice
{
    [PostSubType("channel_created")]
    public class GuildChannelCreatedNotice : GuildNotice
    {
        [JsonProperty("operator_id")]
        public string? OperatorId { get; set; }

        [JsonProperty("channel_info")]
        public ChannelInfo? ChannelInfo { get; set; }
    }
}
