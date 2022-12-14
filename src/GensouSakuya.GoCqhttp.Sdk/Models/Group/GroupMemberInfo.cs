using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Group
{
    public class GroupMemberInfo
    {
        [JsonProperty("group_id")]
        public string? GroupId { get; set; }

        [JsonProperty("user_id")]
        public string? UserId { get; set; }

        [JsonProperty("nickname")]
        public string? NickName { get; set; }

        /// <summary>
        /// 群名片/备注
        /// </summary>
        [JsonProperty("card")]
        public string? Card { get; set; }

        [JsonProperty("sex")]
        public string? Sex { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("area")]
        public string? Area { get; set; }

        [JsonProperty("join_time")]
        public int JoinTime { get; set; }

        [JsonProperty("last_sent_time")]
        public int LastSentTime { get; set; }

        [JsonProperty("level")]
        public string? Level { get; set; }

        [JsonProperty("role")]
        public string? Role { get; set; }

        [JsonProperty("unfriendly")]
        public bool Unfriendly { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("title_expire_time")]
        public long TitleExpireTime { get; set; }

        [JsonProperty("card_changeable")]
        public bool CardChangeable { get; set; }

        [JsonProperty("shut_up_timestamp")]
        public long ShutUpTimestamp { get; set; }
    }
}
