using GensouSakuya.GoCqhttp.Sdk.Models.Guild;
using GensouSakuya.GoCqhttp.Sdk.Sessions.Drivers.Events;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Notice
{
    [PostSubType("message_reactions_updated")]
    public class GuildMessageReactionsUpdatedNotice: GuildNotice
    {
        [JsonProperty("message_id")]
        public string? MessageId { get; set; }

        [JsonProperty("current_reactions")]
        public List<ReactionInfo>? CurrentReactions { get; set; }
    }
}
