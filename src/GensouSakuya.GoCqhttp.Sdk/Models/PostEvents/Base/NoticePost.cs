using GensouSakuya.GoCqhttp.Sdk.Drivers.Events;
using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Base
{
    [PostInfo("notice", "notice_type")]
    public abstract class NoticePost : Post
    {
        [JsonProperty("notice_type")]
        public string? NoticeType { get; set; }
    }
}
