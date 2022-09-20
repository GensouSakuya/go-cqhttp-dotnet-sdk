using GensouSakuya.GoCqhttp.Sdk.Drivers.Events;
using GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Base;

namespace GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Message
{
    [PostSubType("private")]
    public class PrivateMessage : MessagePost
    {
        public int? TempSource { get; set; }

        public bool IsTempChat => !TempSource.HasValue;
    }
}
