using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Sessions.Models.PostEvents.Base
{
    public abstract class Post
    {
        public static readonly string TypePropertyName = "post_type";

        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("self_id")]
        public long SelfId { get; set; }

        [JsonProperty("post_type")]
        public string? PostType { get; set; }
    }
}
