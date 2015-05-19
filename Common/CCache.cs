using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Library.Net.CLCommon
{
    /// <summary>
    /// 抽象工厂的实现，因为反射出实例很耗费性能，所以运用了缓存来减轻负担
    /// </summary>
    public class CCache
    {
        public CCache()
        {

        }
        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static object Get(string key)
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            return cache.Get(key);
        }
        /// <summary>
        /// 添加缓存数据
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public static void InsertCache(string key, object value)
        {
            if (Get(key) == null)
            {
                System.Web.Caching.Cache cache = HttpRuntime.Cache;
                cache.Insert(key, value);
            }
        }
    }
}
