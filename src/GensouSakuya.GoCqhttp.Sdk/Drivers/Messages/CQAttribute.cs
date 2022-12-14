using System;

namespace GensouSakuya.GoCqhttp.Sdk.Drivers.Messages
{
    internal class CQAttribute : Attribute
    {
        public string Type { get; set; }
        public CQAttribute(string type)
        {
            Type = type;
        }
    }
}
