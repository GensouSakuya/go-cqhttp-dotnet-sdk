using GensouSakuya.GoCqhttp.Sdk.Sessions.Drivers.Events;
using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Sessions.Models.PostEvents.Base
{
    [PostInfo("notice", "notice_type")]
    public abstract class NoticePost : Post
    {
        [JsonProperty("notice_type")]
        public string? NoticeType { get; set; }
    }
}
