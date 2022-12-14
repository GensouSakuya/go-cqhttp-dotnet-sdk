using GensouSakuya.GoCqhttp.Sdk.Models.Messages;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GensouSakuya.GoCqhttp.Sdk.Drivers.Messages
{
    internal static class MessageChainConverter
    {
        public static IEnumerable<BaseMessage> ConvertToChain(string? rawMessage)
        {
            if(rawMessage == null)
                return Enumerable.Empty<BaseMessage>();
            var chain = new List<BaseMessage>();
            for(var i = 0;i < rawMessage.Length;i++)
            {
                var j = i;
                if (rawMessage[i] == '[' && rawMessage[i+1] == 'C' && rawMessage[i + 2] == 'Q')
                {
                    for(; j < rawMessage.Length; j++)
                    {
                        if (rawMessage[j] == ']')
                        {
                            break;
                        }
                    }
                    var cqMsg = rawMessage.Substring(i, j - i + 1);
                    chain.Add(ConvertToCqMessage(cqMsg));
                }
                else
                {
                    for (; j < rawMessage.Length; j++)
                    {
                        if (rawMessage[j] == '[')
                            break;
                    }
                    var textMsg = rawMessage.Substring(i, j - i);
                    chain.Add(new TextMessage(textMsg));
                }
                i = j;
            }
            return chain;
        }

        internal static BaseMessage ConvertToCqMessage(string cqMessage)
        {
            string? cqType = null;
            var index = 4;
            for(var i= index; i < cqMessage.Length; i++)
            {
                if (cqMessage[i] == ',')
                {
                    cqType = cqMessage.Substring(4, i-4);
                    index = i;
                    break;
                }
            }
            if (cqType == null)
                throw new Exception($"cq type obtain failed, origin:{cqMessage}");
            index++;
            var obj = new JObject();
            for (var i=index; i < cqMessage.Length; i++)
            {
                string propertyName = null;
                string propertyValue;
                for(var j=i; j < cqMessage.Length; j++)
                {
                    if(cqMessage[j] == '=')
                    {
                        propertyName = cqMessage.Substring(i, j - i);
                        i = j;
                        break;
                    }
                }
                if (propertyName == null)
                    throw new Exception($"cq property name obtain failed, origin:{cqMessage}");
                i++;
                var end = cqMessage.Length-1;
                for(var j=i;j < cqMessage.Length; j++)
                {
                    if (cqMessage[j] == ',' || cqMessage[j] == ']')
                    {
                        end = j;
                        break;
                    }
                } 
                propertyValue = cqMessage.Substring(i, end - i);
                i = end;
                obj.Add(propertyName, propertyValue);
            }
            var type = MessageResolver.GetCQMessageType(cqType);
            var msg = (BaseMessage?)obj.ToObject(type);
            if (msg == null)
                throw new Exception($"cq message convert failed, origin:{cqMessage}");
            return msg;
        }
    }
}
