using GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace GensouSakuya.GoCqhttp.Sdk.Drivers.Events
{
    internal class EventJsonConverter : BaseConverter<Post>
    {
        protected override Post Create(Type objectType, JObject jObj)
        {
            if(FieldExists(Post.TypePropertyName, jObj, out var postType))
            {
                var subTypePropertyName = EventResolver.GetSubTypePropertyName(postType);
                if(FieldExists(subTypePropertyName, jObj, out var subType))
                {
                    return EventResolver.GeneratePost(postType, subType);
                }
                throw new Exception($"json has no sub type, json:{jObj}");
            }
            throw new Exception($"json has no property type, json:{jObj}");
        }

        private bool FieldExists(string fieldName, JObject jObject, out string value)
        {
            value = jObject[fieldName]?.ToString() ?? "";
            return jObject[fieldName] != null;
        }
    }

    internal abstract class BaseConverter<T> : JsonConverter
    {
        protected abstract T Create(Type objectType, JObject jObj);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var jObj = JObject.Load(reader);
            var target = Create(objectType, jObj);
            serializer.Populate(jObj.CreateReader(), target);
            return target;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
