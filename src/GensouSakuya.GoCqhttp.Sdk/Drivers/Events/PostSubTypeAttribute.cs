using System;

namespace GensouSakuya.GoCqhttp.Sdk.Drivers.Events
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
