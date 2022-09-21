using Newtonsoft.Json;
using System.Collections.Generic;

namespace GensouSakuya.GoCqhttp.Sdk.Models
{
    public class ChannelInfo
    {
        [JsonProperty("owner_guild_id")]
        public long OwnerGuildId { get; set; }
        [JsonProperty("channel_id")]
        public long ChannelId { get; set; }
        /// <summary>
        /// 1:文字频道;2:语音频道;5:直播频道;7:主题频道
        /// </summary>
        [JsonProperty("channel_type")]
        public int ChannelType { get; set; }
        [JsonProperty("channel_name")]
        public string? ChannelName { get; set; }
        [JsonProperty("create_time")]
        public long CraeteTime { get; set; }
        [JsonProperty("creator_tiny_id")]
        public long CreatorTinyId { get; set; }
        [JsonProperty("talk_permission")]
        public int TalkPermission { get; set; }
        [JsonProperty("visible_type")]
        public int VisibleType { get; set; }
        [JsonProperty("current_slow_mode")]
        public int CurrentSlowMode { get; set; }
        [JsonProperty("slow_modes")]
        public List<SlowModeInfo>? SlowModes { get; set; }
    }
}
