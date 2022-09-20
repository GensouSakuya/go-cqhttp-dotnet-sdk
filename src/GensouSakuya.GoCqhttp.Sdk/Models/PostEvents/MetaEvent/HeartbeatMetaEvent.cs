using GensouSakuya.GoCqhttp.Sdk.Drivers.Events;
using GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Base;

namespace GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.MetaEvent
{
    [PostSubType("heartbeat")]
    public class HeartbeatMetaEvent : MetaEventPost
    {
    }
}
