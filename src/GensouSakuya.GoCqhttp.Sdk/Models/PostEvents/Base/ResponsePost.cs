using GensouSakuya.GoCqhttp.Sdk.Sessions.Models.PostEvents.Base;
using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Base
{
    internal class ResponsePost: Post
    {
        public static readonly string MainPropertyName = "echo";

        [JsonProperty("echo")]
        public string? Echo { get; set; }
        [JsonProperty("retcode")]
        public int Retcode { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }

        public bool IsSuccess => Retcode == 0;
    }

    internal class ResponsePost<TData>:ResponsePost where TData : class
    {
        [JsonProperty("data")]
        public TData? Data { get; set; }
    }
}
