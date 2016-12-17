using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MvcDemo.Framework.Cache
{
    public static class CacheManager
    {
        public static T Get<T>(string cacheID) where T :
            class
        {
            T item = HttpRuntime.Cache.Get(cacheID) as T;
            if (item == default(T))
                item = MemoryCache.Default.Get(cacheID) as T;

            return item;
        }
        public static T GetOrAdd<T>(string cacheID, Func<T> getItemCallback) 
            where T : class
        {
            T item = Get<T>(cacheID);
            if (item == null)
            {
                item = getItemCallback();
                HttpContext.Current.Cache.Insert(cacheID, item);
            }
            return item;
        }
        public static T GetOrAdd<T>(string cacheID, int duration, 
            Func<T> getItemCallback) where T : class
        {
            T item = MemoryCache.Default.Get(cacheID) as T;
            if (item == null)
            {
                item = getItemCallback();
                MemoryCache.Default.Add(cacheID, item, 
                    DateTime.Now.AddHours(duration));
            }
            return item;
        }
    }
}
