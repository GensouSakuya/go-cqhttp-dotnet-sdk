using Newtonsoft.Json;

namespace GensouSakuya.GoCqhttp.Sdk.Models.Requests
{
    internal class GetGuildChannelListRequest
    {
        public GetGuildChannelListRequest(string guildId, bool noCache)
        {
            GuildId = guildId;
            NoCache = noCache;
        }

        [JsonProperty("guild_id")]
        public string GuildId { get; set; }

        [JsonProperty("no_cache")]
        public bool NoCache { get; set; }
    }
}
