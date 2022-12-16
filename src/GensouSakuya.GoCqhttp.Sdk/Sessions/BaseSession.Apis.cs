using System.Collections.Generic;
using System.Threading.Tasks;
using GensouSakuya.GoCqhttp.Sdk.Models.Group;
using GensouSakuya.GoCqhttp.Sdk.Models.Guild;
using GensouSakuya.GoCqhttp.Sdk.Models.Responses;

namespace GensouSakuya.GoCqhttp.Sdk.Sessions
{
    public partial class BaseSession
    {
        /// <summary>
        /// 发送私聊消息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="message"></param>
        /// <param name="autoEscape"></param>
        /// <param name="tempFromGroupId">主动发起临时会话的来源群号</param>
        /// <returns></returns>
        public abstract Task<string> SendPrivateMessage(string userId, string message, bool autoEscape = false, string? tempFromGroupId = null);

        #region Group
        public abstract Task<List<GroupMemberInfo>> GetGroupMemberList(string groupId, bool noCache = true);
        public abstract Task<string> SendGroupMessage(string groupId, string message, bool autoEscape = false);
        #endregion

        #region Guild
        public abstract Task<GuildProfile?> GetGuildServiceProfile();

        public abstract Task<List<GuildInfo>?> GetGuildList();

        public abstract Task<GuildGuestMeta?> GetGuildMetaByGuest(string guildId);

        public abstract Task<List<ChannelInfo>?> GetGuildChannelList(string guildId, bool noCache);

        public abstract Task<GetGuildMemberListResponse> GetGuildMemberList(string guildId, string nextToken);

        public abstract Task<GuildMemberInfo?> GetGuildMemberProfile(string guildId, string tinyId);

        public abstract Task<string> SendGuildChannelMsg(string guildId, string channelId, string msg);

        public abstract Task<List<FeedInfo>?> GetTopicChannelFeeds(string guildId, string channelId);

        public abstract Task DeleteGuildRole(string guildId, string roleId);

        public abstract Task<List<RoleInfo>> GetGuildRoles(string guildId);

        public abstract Task SetGuildMemberRole(string guildId, bool set, string roleId, List<string> users);

        public abstract Task UpdateGuildRole(string guildId, string roleId, string name, string color, bool independent);

        public abstract Task<string> CreateGuildRole(string guildId, string color, string name, bool independent, List<string> initialUsers);
        #endregion
    }
}
