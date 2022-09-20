using GensouSakuya.GoCqhttp.Sdk.Models.Responses.Guild;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace GensouSakuya.GoCqhttp.Sdk
{
    public partial class Session
    {
        private Uri _httpUri;
        private RestClient _httpClient;

        public async Task<GuildMemberInfo?> GetGuildMemberProfile(long guildId, long userId)
        {
            var request = new RestRequest("/get_guild_member_profile");
            request.Method = Method.Get;
            request.AddParameter("guild_id", guildId);
            request.AddParameter("user_id", userId);
            var response = await _httpClient.ExecuteAsync<GuildMemberInfo>(request);
            response.ThrowIfError();
            return response.Data;
        }
    }
}
