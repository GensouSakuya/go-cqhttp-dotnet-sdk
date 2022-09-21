using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GensouSakuya.GoCqhttp.Sdk.Sessions.Models.PostEvents.Base;

namespace GensouSakuya.GoCqhttp.Sdk.Sessions
{
    public partial class BaseSession
    {
        internal static Dictionary<Type, Func<BaseSession, dynamic>> _handlerResolverMaps = new Dictionary<Type, Func<BaseSession, dynamic>>();

        public delegate Task AsyncEventHandler<T>(object sender, T args);

        private static void PrepareHandlerAutoMap()
        {
            var eventTypeName = typeof(AsyncEventHandler<>).FullName;
            var sessionType = typeof(BaseSession);
            var fields = sessionType.GetProperties();
            var events = fields.Where(p => p.PropertyType.FullName.StartsWith(eventTypeName)).ToList();
            foreach (var e in events)
            {
                var getter = e.GetGetMethod();
                var pa = Expression.Parameter(sessionType);
                var body = Expression.Call(pa, getter);

                _handlerResolverMaps[e.PropertyType.GenericTypeArguments[0]] = Expression.Lambda<Func<BaseSession, dynamic>>(body, pa).Compile();
            }
        }


        protected async Task InvokeHandler(Post post)
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
