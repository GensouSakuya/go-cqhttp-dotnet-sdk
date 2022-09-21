using GensouSakuya.GoCqhttp.Sdk.Sessions.Drivers.Events;
using GensouSakuya.GoCqhttp.Sdk.Sessions.Models.PostEvents.Base;

namespace GensouSakuya.GoCqhttp.Sdk.Sessions.Models.PostEvents.MetaEvent
{
    [PostSubType("heartbeat")]
    public class HeartbeatMetaEvent : MetaEventPost
    {
    }
}
