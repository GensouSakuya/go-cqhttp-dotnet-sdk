using System;

namespace GensouSakuya.GoCqhttp.Sdk.Sessions.Drivers.Events
{
    internal class PostSubTypeAttribute : Attribute
    {
        public string SubType { get; }

        public PostSubTypeAttribute(string subType)
        {
            SubType = subType;
        }
    }
}
