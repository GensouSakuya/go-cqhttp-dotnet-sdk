using GensouSakuya.GoCqhttp.Sdk.Sessions.Drivers.Events;
using GensouSakuya.GoCqhttp.Sdk.Sessions.Models.PostEvents.Base;

namespace GensouSakuya.GoCqhttp.Sdk.Sessions.Models.PostEvents.Message
{
    [PostSubType("private")]
    public class PrivateMessage : MessagePost
    {
        public int? TempSource { get; set; }

        public bool IsTempChat => !TempSource.HasValue;
    }
}
