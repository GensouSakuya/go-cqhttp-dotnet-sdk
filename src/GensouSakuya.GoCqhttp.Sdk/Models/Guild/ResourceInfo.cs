using Newtonsoft.Json;
using System.Collections.Generic;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Guild
{
    public class ResourceInfo
    {
        [JsonProperty("images")]
        public List<FeedMedia>? Images { get; set; }

        [JsonProperty("videos")]
        public List<FeedMedia>? Videos { get; set; }
    }
}
