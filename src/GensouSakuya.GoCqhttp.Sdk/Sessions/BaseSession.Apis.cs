using System.Threading.Tasks;
using GensouSakuya.GoCqhttp.Sdk.Sessions.Models.Responses.Guild;

namespace GensouSakuya.GoCqhttp.Sdk.Sessions
{
    public partial class BaseSession
    {
        public abstract Task<GuildMemberInfo?> GetGuildMemberProfile(long guildId, long tinyId);
    }
}
