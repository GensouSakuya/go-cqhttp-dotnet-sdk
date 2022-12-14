using GensouSakuya.GoCqhttp.Sdk.Models.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace GensouSakuya.GoCqhttp.Sdk
{
    public static class Extensions
    {
        public static string ToRawMessage(this IEnumerable<BaseMessage> message)
        {
            if (message == null)
                return string.Empty;
            var builder = new StringBuilder();
            foreach (var messageItem in message)
            {
                builder.Append(messageItem.RawText);
            }
            return builder.ToString();
        }
    }
}
