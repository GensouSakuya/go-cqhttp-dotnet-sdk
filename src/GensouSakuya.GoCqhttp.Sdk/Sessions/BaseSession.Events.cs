using GensouSakuya.GoCqhttp.Sdk.Sessions.Models.PostEvents.Message;

namespace GensouSakuya.GoCqhttp.Sdk.Sessions
{
    public partial class BaseSession
    {
        public AsyncEventHandler<PrivateMessage>? PrivateMessageReceived { get; set; }
        public AsyncEventHandler<GroupMessage>? GroupMessageReceived { get; set; }
        public AsyncEventHandler<GuildMessage>? GuildMessageReceived { get; set; }
    }
}
