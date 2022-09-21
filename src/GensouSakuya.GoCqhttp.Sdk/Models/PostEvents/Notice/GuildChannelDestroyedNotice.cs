using GensouSakuya.GoCqhttp.Sdk.Sessions.Drivers.Events;
using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Notice
{
    [PostSubType("channel_destroyed")]
    public class GuildChannelDestroyedNotice : GuildNotice
    {
        [JsonProperty("operator_id")]
        public long OperatorId { get; set; }

        [JsonProperty("channel_info")]
        public ChannelInfo? ChannelInfo { get; set; }
    }
}
