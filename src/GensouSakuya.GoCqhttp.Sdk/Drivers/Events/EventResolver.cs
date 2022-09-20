using GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GensouSakuya.GoCqhttp.Sdk.Drivers.Events
{
    internal static class EventResolver
    {
        private static readonly Type _basePostType = typeof(Post);
        private static readonly Dictionary<string, PostSubClassInfo> _postEventTypeMap;
        static EventResolver()
        {
            _postEventTypeMap = new Dictionary<string, PostSubClassInfo>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(p=>!p.FullName.StartsWith("System.") && !p.FullName.StartsWith("Microsoft.") && !p.FullName.StartsWith("Anonymously Hosted"));
            foreach(var assembly in assemblies)
            {
                foreach(var type in assembly.ExportedTypes)
                {
                    if(type != _basePostType && _basePostType.IsAssignableFrom(type))
                    {
                        var postAttr = type.GetCustomAttribute<PostInfoAttribute>(false);
                        string postType;
                        if (postAttr != null)
                        {
                            var subInfo = GetSubClassInfo(postAttr.PostType);
                            subInfo.PostType = postAttr.PostType;
                            subInfo.SubTypePropertyName = postAttr.SubTypePropertyName;
                            postType = postAttr.PostType;
                        }
                        else
                        {
                            var inheritPostAttr = type.GetCustomAttribute<PostInfoAttribute>(true);
                            postType = inheritPostAttr.PostType;
                        }
                        var eventAttr = type.GetCustomAttribute<PostSubTypeAttribute>(false);
                        if (eventAttr != null)
                        {
                            var subInfo = GetSubClassInfo(postType);
                            subInfo.EventTypes[eventAttr.SubType] = type;
                        }
                    }
                }
            }
        }

        public static string GetSubTypePropertyName(string postType)
        {
            if (!_postEventTypeMap.ContainsKey(postType))
                throw new Exception($"unknown post type:{postType}");
            return _postEventTypeMap[postType].SubTypePropertyName;
        }

        public static Post GeneratePost(string postType, string subType)
        {
            if (!_postEventTypeMap.ContainsKey(postType))
                throw new Exception($"unknown post type:{postType}");
            if (!_postEventTypeMap[postType].EventTypes.ContainsKey(subType))
                throw new Exception($"unknown sub type[{subType}] of post[{postType}]");
            return (Post)Activator.CreateInstance(_postEventTypeMap[postType].EventTypes[subType]);
        }

        private static PostSubClassInfo GetSubClassInfo(string postType)
        {
            if (!_postEventTypeMap.ContainsKey(postType))
                _postEventTypeMap[postType] = new PostSubClassInfo();
            return _postEventTypeMap[postType];
        }

        internal class PostSubClassInfo
        {
            public string PostType { get; internal set; } = string.Empty;
            public string SubTypePropertyName { get; internal set; } = string.Empty;
            public Dictionary<string, Type> EventTypes { get; } = new Dictionary<string, Type>();
        }
    }
}
