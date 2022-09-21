using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models
{
    public class SlowModeInfo
    {
        [JsonProperty("slow_mode_key")]
        public int SlowModeKey { get; set; }
        [JsonProperty("slow_mode_text")]
        public string? SlowModeText { get; set; }
        [JsonProperty("speak_frequency")]
        public int SpeakFrequency { get; set; }
        [JsonProperty("slow_mode_circle")]
        public int SlowModeCircle { get; set; }
    }
}
