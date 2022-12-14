using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GensouSakuya.GoCqhttp.Sdk.Models.Messages;

namespace GensouSakuya.GoCqhttp.Sdk.Drivers.Messages
{
    internal static class MessageResolver
    {
        private static readonly Type _baseMessageType = typeof(BaseMessage);
        private static readonly Dictionary<string, Type> _cqMessageType;
        static MessageResolver()
        {
            _cqMessageType = new Dictionary<string, Type>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.FullName.StartsWith("System.") && !p.FullName.StartsWith("Microsoft.") && !p.FullName.StartsWith("Anonymously Hosted"));
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.ExportedTypes)
                {
                    if (type != _baseMessageType && _baseMessageType.IsAssignableFrom(type))
                    {
                        var typeAttr = type.GetCustomAttribute<CQAttribute>(false);
                        if (typeAttr != null)
                        {
                            _cqMessageType[typeAttr.Type] = type;
                        }
                    }
                }
            }
        }

        public static Type GetCQMessageType(string type)
        {
            if (!_cqMessageType.ContainsKey(type))
                throw new Exception($"unknown cq type:{type}");
            return _cqMessageType[type];
        }
    }
}
