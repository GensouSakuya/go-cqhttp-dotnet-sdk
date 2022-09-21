using System.Collections.Generic;
using System.Threading.Tasks;
using GensouSakuya.GoCqhttp.Sdk.Models.Guild;

namespace GensouSakuya.GoCqhttp.Sdk.Sessions
{
    public partial class BaseSession
    {
        public abstract Task<GuildProfile?> GetGuildServiceProfile();

        public abstract Task<List<GuildInfo>?> GetGuildList();

        public abstract Task<GuildGuestMeta?> GetGuildMetaByGuest(string guildId);

        public abstract Task<List<ChannelInfo>?> GetGuildChannelList(string guildId, bool noCache);

        public abstract Task<List<GuildMemberInfo>> GetGuildMemberList(string guildId, string nextToken);

        public abstract Task<GuildMemberInfo?> GetGuildMemberProfile(string guildId, string tinyId);

        public abstract Task<string?> SendGuildChannelMsg(string guildId, string channelId, string msg);

        public abstract Task<List<FeedInfo>?> GetTopicChannelFeeds(string guildId, string channelId);

        public abstract Task DeleteGuildRole(string guildId, string roleId);

        public abstract Task<List<RoleInfo>> GetGuildRoles(string guildId);

        public abstract Task SetGuildMemberRole(string guildId, bool set, string roleId, List<string> users);

        public abstract Task UpdateGuildRole(string guildId, string roleId, string name, string color, bool independent);

        public abstract Task<string> CreateGuildRole(string guildId, string color, string name, bool independent, List<string> initialUsers);
    }
}
