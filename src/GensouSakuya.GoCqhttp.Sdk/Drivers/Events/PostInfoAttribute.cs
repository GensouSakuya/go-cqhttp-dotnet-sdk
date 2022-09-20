using System;

namespace GensouSakuya.GoCqhttp.Sdk.Drivers.Events
{
    internal class PostInfoAttribute : Attribute
    {
        public string PostType { get; }
        public string SubTypePropertyName { get; }

        public PostInfoAttribute(string postType, string subTypePropertyName)
        {
            PostType = postType;
            SubTypePropertyName = subTypePropertyName;
        }
    }
}
