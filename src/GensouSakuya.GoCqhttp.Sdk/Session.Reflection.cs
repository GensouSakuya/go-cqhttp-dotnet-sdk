using GensouSakuya.GoCqhttp.Sdk.Models.PostEvents.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GensouSakuya.GoCqhttp.Sdk
{
    public partial class Session
    {
        internal static Dictionary<Type, Func<Session, dynamic>> _handlerResolverMaps = new Dictionary<Type, Func<Session, dynamic>>();

        public delegate Task AsyncEventHandler<T>(object sender, T args);

        private static void PrepareHandlerAutoMap()
        {
            var eventTypeName = typeof(AsyncEventHandler<>).FullName;
            var sessionType = typeof(Session);
            var fields = sessionType.GetProperties();
            var events = fields.Where(p => p.PropertyType.FullName.StartsWith(eventTypeName)).ToList();
            foreach (var e in events)
            {
                var getter = e.GetGetMethod();
                var pa = Expression.Parameter(sessionType);
                var body = Expression.Call(pa, getter);

                _handlerResolverMaps[e.PropertyType.GenericTypeArguments[0]] = Expression.Lambda<Func<Session, dynamic>>(body, pa).Compile();
            }
        }


        private async Task InvokeHandler(Post post)
        {
            var postType = post.GetType();
            if (_handlerResolverMaps.ContainsKey(postType))
            {
                var ff = _handlerResolverMaps[postType].Invoke(this);
                if (ff != null)
                {
                    await ff.DynamicInvoke(this, post);
                }
            }
        }
    }
}
