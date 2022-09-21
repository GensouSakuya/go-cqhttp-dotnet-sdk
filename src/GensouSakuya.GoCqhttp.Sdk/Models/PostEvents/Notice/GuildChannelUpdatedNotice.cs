using GensouSakuya.GoCqhttp.Sdk.Sessions.Drivers.Events;
using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Notice
{
    [PostSubType("channel_updated")]
    public class GuildChannelUpdatedNotice : GuildNotice
    {
        [JsonProperty("operator_id")]
        public long OperatorId { get; set; }

        [JsonProperty("old_info")]
        public ChannelInfo? OldInfo { get; set; }

        [JsonProperty("new_info")]
        public ChannelInfo? NewInfo { get; set; }
    }
}
