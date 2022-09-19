using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Events
{
    internal class BaseEvent
    {
        [JsonProperty("time")]
        public long Time { get; set; }

        [JsonProperty("self_id")]
        public long SelfId { get; set; }
    }
}
